using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace npl_training_demo_cs
{
    public partial class MainWindow : Window
    {
        // ML.NET context for model training and predictions
        private readonly MLContext mlContext;

        // Stores training examples
        private List<SentimentData> trainingExamples;

        // Prediction engine to analyze user input
        private PredictionEngine<SentimentData, SentimentPrediction> predictionEngine;

        // Sentiment input schema
        private class SentimentData
        {
            public string Text { get; set; }
            public bool Label { get; set; }
        }

        // Sentiment prediction output schema
        private class SentimentPrediction
        {
            [ColumnName("PredictedLabel")]
            public bool Prediction { get; set; }

            public float Probability { get; set; }
            public float Score { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

            // Create ML.NET context
            mlContext = new MLContext();

            // Load initial training examples
            trainingExamples = new List<SentimentData>
            {
                new SentimentData { Text = "I am happy", Label = true },
                new SentimentData { Text = "I hate this", Label = false },
                new SentimentData { Text = "I am sad", Label = false },
                new SentimentData { Text = "I am good", Label = true }
            };

            // Train the initial model
            TrainModel();
        }

        /// <summary>
        /// Trains the model using current training examples.
        /// </summary>
        private void TrainModel()
        {
            var dataView = mlContext.Data.LoadFromEnumerable(trainingExamples);

            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
                    labelColumnName: "Label",
                    featureColumnName: "Features"));

            var trainedModel = pipeline.Fit(dataView);

            predictionEngine = mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(trainedModel);
        }

        /// <summary>
        /// Handles click on "Analyze Sentiment" button.
        /// </summary>
        private void AnalyzeSentiment(object sender, RoutedEventArgs e)
        {
            string inputText = emotionTextBox.Text?.Trim();

            if (string.IsNullOrWhiteSpace(inputText))
            {
                MessageBox.Show("Please enter how you're feeling.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Perform prediction
            var result = predictionEngine.Predict(new SentimentData { Text = inputText });

            // Compute positive/negative percentage
            float positivePercent = result.Probability * 100;
            float negativePercent = 100 - positivePercent;
            string sentiment = result.Prediction ? "Positive" : "Negative";

            // Construct display message
            string feedback = $"Detected: {sentiment} emotion\n" +
                              $"Positive: {positivePercent:F1}%\n" +
                              $"Negative: {negativePercent:F1}%\n\n";

            string suggestion;

            if (positivePercent > 75)
            {
                suggestion = "😊 You seem really upbeat! Keep shining!";
            }
            else if (positivePercent > 50)
            {
                suggestion = "🙂 You're doing alright — keep your chin up!";
            }
            else if (positivePercent > 30)
            {
                suggestion = "😐 Feeling a bit low? It's okay, tough days pass.";
            }
            else
            {
                suggestion = "😔 You seem down. Be kind to yourself — brighter days are ahead.";
            }

            resultTextBlock.Text = feedback + suggestion;

            // Ask user for feedback
            var userConfirmation = MessageBox.Show("Was this prediction accurate?", "Feedback", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (userConfirmation == MessageBoxResult.No)
            {
                var correct = MessageBox.Show("Was it actually a POSITIVE emotion?", "Correction", MessageBoxButton.YesNo, MessageBoxImage.Question);
                bool correctLabel = (correct == MessageBoxResult.Yes);

                // Learn from user correction
                trainingExamples.Add(new SentimentData { Text = inputText, Label = correctLabel });
                TrainModel();

                MessageBox.Show("Thanks! I've learned from that.");
            }
        }
    }
}

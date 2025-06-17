# NPL Sentiment Trainer

## Overview

The NPL Sentiment Trainer is a WPF application that utilizes 
ML.NET to analyze and predict the sentiment of user input text. 
The application allows users to input their feelings, and it 
provides feedback on whether the sentiment is positive or 
negative, along with a confidence score. Users can also provide 
feedback to improve the model over time.

## Features

- **Sentiment Analysis**: The application predicts whether the input text expresses a positive or negative sentiment.
- **User  Feedback**: Users can confirm the accuracy of the prediction, allowing the model to learn from corrections.
- **Dynamic Training**: The model is retrained with new examples based on user feedback, improving its accuracy over time.
- **User -Friendly Interface**: A simple and intuitive interface for entering text and viewing results.

## Technologies Used

- **C#**: The primary programming language for the application.
- **WPF (Windows Presentation Foundation)**: For building the user interface.
- **ML.NET**: A machine learning framework for .NET to handle model training and predictions.

## Getting Started

### Prerequisites

- .NET SDK (version 5.0 or later)
- Visual Studio (or any compatible IDE)
- ML.NET NuGet package

### Installation

1. **Clone the Repository**: 
   ```bash
   git clone https://github.com/HChristopherNaoyuki/npl-training-demo-cs.git
   ```

2. **Open the Project**: Open the solution file (`.sln`) in Visual Studio.

3. **Install Dependencies**: Ensure that the ML.NET NuGet package is installed. You can do this via the NuGet Package Manager:
   - Right-click on the project in Solution Explorer.
   - Select "Manage NuGet Packages".
   - Search for `Microsoft.ML` and install it.

4. **Build the Project**: Build the solution to restore any missing dependencies.

5. **Run the Application**: Start the application by pressing `F5` or clicking on the "Start" button in Visual Studio.

## Usage

1. **Input Text**: Enter your emotion or feeling in the text box.
2. **Analyze Sentiment**: Click the "Analyze Sentiment" button to get the prediction.
3. **View Results**: The application will display whether the sentiment is positive or negative, along with the confidence percentages.
4. **Provide Feedback**: After the prediction, you can confirm if the prediction was accurate. If not, you can specify whether the actual sentiment was positive or negative, which will help improve the model.

## Code Structure

- **MainWindow.xaml.cs**: Contains the logic for the application, including model training and sentiment analysis.
- **MainWindow.xaml**: Defines the user interface layout and elements.

### Key Classes

- **SentimentData**: Represents the input data structure for sentiment analysis, containing the text and its label (true for positive, false for negative).
  
- **SentimentPrediction**: Represents the output of the sentiment prediction, including the predicted label, probability, and score.

### Important Methods

- **TrainModel()**: Trains the sentiment analysis model using the current training examples.
- **AnalyzeSentiment()**: Handles the button click event to analyze the sentiment of the input text.

## Acknowledgments

- Thanks to the ML.NET team for providing a powerful machine learning framework.
- Special thanks to the open-source community for their contributions and support.

---

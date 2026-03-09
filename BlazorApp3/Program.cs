using System.Collections.Generic;
using System;

public interface IGradeStrategy
{
    string StrategyName { get; }
    double CalculateFinalGrade(List<int> assignments, int ExamScore);
}

public class StandartGradind : IGradeStrategy
{
    private string _strategyName = "Standart grading";
    public string StrategyName => _strategyName;
    public double CalculateFinalGrade(List<int> assignments, int ExamScore)
    {
        double sum = 0;
        double average = 0;
        
        if (assignments.Count > 0)
        {
            foreach (int grade in assignments)
            {
                sum += grade;
            }
            average = sum / assignments.Count;
        }
        return (average * 0.4) + (ExamScore * 0.6);
    }
}

public class PracticalGrading : IGradeStrategy
{
    private string _strategyName = "Practical Grading";
    public string StrategyName => _strategyName;

    public double CalculateFinalGrade(List<int> assignments, int ExamScore)
    {
        double sum = 0;
        double average = 0;

        if (assignments.Count > 0)
        {
            foreach (int grade in assignments)
            {
                sum += grade;
            }

            average = sum / assignments.Count;
        }

        return average * 0.8 + ExamScore * 0.2;
    }
}

public class ExamOnlyGrading : IGradeStrategy
{
    private string _strategyName = "Exam grading";
    public string StrategyName => _strategyName;

    public double CalculateFinalGrade(List<int> assignments, int ExamScore)
    {
        return ExamScore;
    }
}

public class GradebookManager
{
    private Dictionary<string, IGradeStrategy> _strategies = new Dictionary<string, IGradeStrategy>();

    public void RegisterStrategy(string courseCode, IGradeStrategy strategy)
    {
        _strategies[courseCode] = strategy;
    }

    public double GetStudentScore(string courseCode, List<int> grades, int exam)
    {
        if (!_strategies.ContainsKey(courseCode))
        {
            throw new KeyNotFoundException($"Course code '{courseCode}' not found in registered strategies.");
        }

        if (exam < 0 || exam > 100)
        {
            throw new ArgumentException("Exam score must be between 0 and 100.");
        }

        if (grades.Count > 0)
        {
            foreach (int grade in grades)
            {
                if (grade < 0 || grade > 100)
                {
                    throw new ArgumentException("All assignment grades must be between 0 and 100.");
                }
            }
        }

        return _strategies[courseCode].CalculateFinalGrade(grades, exam);
    }
}
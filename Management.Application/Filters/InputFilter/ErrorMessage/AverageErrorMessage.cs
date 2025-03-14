namespace Management.Filters.InputFilter.ErrorMessage;

public static class AverageErrorMessage
{
    public const string DisciplineIdEmpty = "The 'DisciplineId' field cannot be empty";
    public const string DisciplineIdInvalid = "Invalid DisciplineId";
    public const string StudentIdEmpty = "The 'StudentId' field cannot be empty";
    public const string StudentIdInvalid = "Invalid StudentId";
    public const string FirstScoreEmpty = "The 'FirstScore' field cannot be empty";
    public const string SecondScoreEmpty = "The 'SecondScore' field cannot be empty";
    public const string ScoresEmpty = "The 'Scores' field cannot be empty";
    public const string FirstScoreScoreExceededMaximumValue = "The first score cannot exceed {0}.";
    public const string SecondScoreExceededMaximumValue = "The second score cannot exceed {0}.";
  
}

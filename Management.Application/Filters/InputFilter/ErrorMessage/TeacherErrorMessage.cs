namespace Management.Filters.InputFilter.ErrorMessage
{
    public static class TeacherErrorMessage
    {
        public const string FullNameEmpty = "The 'FullName' field is cannot be empty.";
        public const string NameExceededMaximumSize = "the name field cannot exceed {0} characters";
        
        public const string DateBirthEmpty = "The 'DateOfBirth' field is cannot be empty.";
        
        public const string CpfEmpty = "The 'CPF' field is cannot be empty.";
        public const string InvalidCpf = "invalid CPF";
        
        public const string ClassroomIdEmpty = "The 'ClassroomId' field is cannot be empty.";
        public const string ClassroomIdIsInvalid = "Invalid is ClassroomId";
        
        public const string DisciplineIdEmpty = "The 'DisciplineId' field is cannot be empty.";
        public const string DisciplineIdIsInvalid = "Invalid is DisciplineId";

        public const string TeacherContactEmpty = "The 'TeacherContact' field is cannot be empty.";
        
        public const string SalaryEmpty = "The 'Salary' field is cannot be empty";

    }
}
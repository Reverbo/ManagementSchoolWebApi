namespace Management.Common.Erros;

public class ErrorResponse
{
    public required string Code { get; set; }
    public required string Message { get; set; }
    public required List<ErrorDetail> Details { get; set; }
}
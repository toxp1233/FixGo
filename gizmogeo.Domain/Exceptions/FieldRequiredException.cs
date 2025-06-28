namespace gizmogeo.Domain.Exceptions;

public class FieldRequiredException(string message) : Exception($"{message}");

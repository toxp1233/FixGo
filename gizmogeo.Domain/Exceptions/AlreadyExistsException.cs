﻿namespace gizmogeo.Domain.Exceptions;


public class AlreadyExistsException(string Class, string Content) : Exception($"{Class} with this {Content} Exists,");


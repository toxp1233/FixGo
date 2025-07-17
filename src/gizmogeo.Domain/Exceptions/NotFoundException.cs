﻿namespace gizmogeo.Domain.Exceptions;

public class NotFoundException(string resourceType, string resourceIdentifier)
    : Exception($"{resourceType} with: {resourceIdentifier} doesn't exist");

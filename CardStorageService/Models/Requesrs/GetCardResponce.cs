﻿namespace CardStorageService.Models.Requesrs
{
    public class GetCardResponce : IOperationResult
    {
        public IList<CardDto>? Cards { get; set; }
        public int ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
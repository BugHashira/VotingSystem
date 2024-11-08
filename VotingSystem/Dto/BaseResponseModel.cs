﻿namespace VotingSystem.Dto
{
    public class BaseResponseModel<T>
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}

﻿namespace BlazorControlCefa.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;

        public DateTime Now => DateTime.Now;
    }
}

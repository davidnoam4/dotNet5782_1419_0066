﻿using System;

namespace DO
{
    public struct DroneCharge
    {
        public int DroneId { get; set; }
        public int StationId { get; set; }
        public DateTime? StartCharging { get; set; }
        public bool deleted { get; set; }
        public override string ToString()
        {
            return $"DroneId #{DroneId}: StationId={StationId}";
        }
    }
}

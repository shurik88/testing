using System;

namespace TestWebApp2
{
    /// <summary>
    ///     Прогноз погоды
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        ///     Дата
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Температура по Цельсию
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        ///     Температура по Фаренгейту
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        ///     Подробно
        /// </summary>
        public string Summary { get; set; }
    }
}

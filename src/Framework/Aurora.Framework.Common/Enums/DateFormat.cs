namespace Aurora.Framework
{
    /// <summary>
    /// Define las opciones de formato de fecha para un objeto System.DateTime.
    /// </summary>
    public enum DateFormat
    {
        /// <summary>
        /// Formato día-mes.
        /// </summary>
        DayMonth,

        /// <summary>
        /// Formato día-mes-año.
        /// </summary>
        DayMonthYear,

        /// <summary>
        /// Formato mes-día-año.
        /// </summary>
        MonthDayYear,

        /// <summary>
        /// Formato mes-año.
        /// </summary>
        MonthYear,

        /// <summary>
        /// Formato año-día-mes.
        /// </summary>
        YearDayMonth,

        /// <summary>
        /// Formato año-mes.
        /// </summary>
        YearMonth,

        /// <summary>
        /// Formato año-mes-día.
        /// </summary>
        YearMonthDay
    }
}
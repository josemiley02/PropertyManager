using System;

namespace PropertyManagement.Application;

    public static class Constants
    {
        #region Pagination
        public const string PageSize = "PageSize";
        public const string PageNumber = "PageNumber";
        public const string PageNumberMustBeAtLeastOneError = "El número de página debe ser mayor o igual a 1.";
        public const string PageSizeMustBeAtLeastOneError = "El tamaño de página debe ser mayor o igual a 1.";
        #endregion
    }

namespace EMP.Http
{
    public class ExternalAPI
    {
        public static class BillAPI
        {
            /// <summary>
            /// Отримання всіх закритих рахунків поточної зміни
            /// </summary>
            /// <returns></returns>
            public static string GetClosedBills(string baseUri) => $"{baseUri}/***Bills***";

            /// <summary>
            /// Повертає закриті рахуноки по фільтру дат
            /// </summary>
            /// <param name="baseUri"></param>
            /// <returns></returns>
            public static string UploadsCloseBills(string baseUri) => $"{baseUri}/***Bills***";
        }


        public static class UserAPI
        {
            public static string GetAllWaiters(string baseUri) => $"{baseUri}/***Waiters***";
        }

        public class Authenticate
        {
            public static string GetAuthenticate(string baseUri) => $"{baseUri}/***Auth***";
        }

    }
}

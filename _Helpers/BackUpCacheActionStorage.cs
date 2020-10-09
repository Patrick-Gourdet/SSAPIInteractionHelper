using System.IO;
/// Patrick Gourdet Iron Digital Iron Financials 07/3/2020

namespace SSAPIInteractionHelper._Helpers
{
    /// This will be for cacheing the symboles    
    public struct  Files
    {
        
        System.IO.FileStream _accountInfoRw;
        System.IO.FileStream _buySellDayBufferRw;
        System.IO.FileStream _buySellDayIncrementalRw;

         internal Files(FileStream a,FileStream b,FileStream c)
         {
             _accountInfoRw = a;
             _buySellDayBufferRw = b;
             _buySellDayIncrementalRw = c;
         }
    }
     static  class FileReadWriteSingleton
     { 
         private static object _lock = new object();
         private static Files _files;
        const string AccountInfo = @""; 
        const string BuySellDay = @"";
        private const string BeforeMarketOpenPositions = @"";
        private static Files files = new Files(
                                            File.Create(AccountInfo), 
                                            System.IO.File.Create(BuySellDay),
                                            System.IO.File.Create(BuySellDay)
                                            );
        private static void EndOfDayWrite()
        {
            using System.IO.FileStream endODayWrite = System.IO.File.Create(AccountInfo);
        }
        private static void OpenDayRead()
        {
            using System.IO.FileStream EndODayWrite = System.IO.File.Create(BeforeMarketOpenPositions);
        }
     }
     /// <summary>
     /// This is the lock to make the reading from the data pipline thread safe for any data type
     /// that does not support thread safety 
     /// </summary>
    public class BackUpCacheActionStorage
    {
        private object _lock = new object();
    }
}

using System;
using TwinCAT.Ads;
using TwinCAT.TypeSystem;

namespace tcHandler
{
    public class TwinCATClient
    {
        private AdsClient _tcClient = null;


        public void InitializeConnection()
        {
            _tcClient = new AdsClient();
            _tcClient.Connect(AmsNetId.Local, 851);
            if (_tcClient.IsConnected)
            {
                Console.Write("Twin CAT ADS port connected");
            }
            else
            {
                Console.Write("ADS Connection failed");
            }
        }
        public bool ReadBool(string variableName)
        {
            try
            { 
                var hVar = _tcClient.CreateVariableHandle(variableName);
                var readVariable = _tcClient.ReadAny(hVar, typeof(bool));
                _tcClient.DeleteVariableHandle(hVar);
                return bool.Parse(readVariable.ToString());
            }
            catch (AdsErrorException exp)
            {
                Console.Write("TC Read Bool Error  " + exp.Message);
                return false;
            }
        }

        public int ReadInt(string variableName)
        {
            int value = 0;
            try
            {
                var hVar = _tcClient.CreateVariableHandle(variableName);
                value = (int)_tcClient.ReadAny(hVar, typeof(int));
                _tcClient.DeleteVariableHandle(hVar);
            }
            catch (AdsErrorException exp)
            {
                Console.Write("TwinCAT Read Int Error - " + exp.Message);
            }
            return (int)value;
        }

        public bool WriteBool(string variableName, bool value)
        {
            try
            {
                var hVar = _tcClient.CreateVariableHandle(variableName);
                _tcClient.WriteAny(hVar, value);
                _tcClient.DeleteVariableHandle(hVar);
                return true;
            }
            catch (AdsErrorException exp)
            {
                Console.Write("TwinCAT Write Error - " + exp.Message);
            }
            return false;
        }
        public bool WriteInt(string variableName, int value)
        {
            try
            {
                var hVar = _tcClient.CreateVariableHandle(variableName);
                _tcClient.WriteAny(hVar, value);
                _tcClient.DeleteVariableHandle(hVar);
                return true;
            }
            catch (AdsErrorException exp)
            {
                Console.Write("TwinCAT Write Error - " + exp.Message);
            }
            return false;
        }
    }
}

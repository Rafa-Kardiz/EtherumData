using EtherumData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Numerics;
using System.Text;

namespace EtherumData.Services
{
    public class EthereumService
    {

        private readonly EthereumDataContext _context;
        private readonly string url = "https://sepolia.gateway.tenderly.co/";

        public EthereumService(EthereumDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<(Transaction?, string?)> GetTransactionHash(string hash)
        {
            try
            {
                //Se crea el json para la transaccion 
                string jsonRequest = $"{{ \"jsonrpc\": \"2.0\", \"method\": \"eth_getTransactionByHash\", \"params\": [\"{hash}\"], \"id\": 1 }}";

                // Realizamos el llamado al endpoint de etherum
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    string result = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(result);
                    //Evaluamos si existe algun error
                    if (jsonResponse["error"] != null)
                    {
                        return (null, "Ocurrio un error de formato");
                    }
                    //Evaluamos el resultado que no sea nulo 
                    if (jsonResponse["result"] == null || !jsonResponse["result"].HasValues)
                    {
                        return (null, "No se obtuvo información de este hash: " + hash);
                    }

                    var resultEth = jsonResponse["result"];

                    Transaction transaction = new Transaction
                    {
                        Id = resultEth["hash"]?.ToString(),
                        Value = TranformHexToWei(resultEth["value"]?.ToString()),
                        GasUsed = TransformHextoDec(resultEth["gas"]?.ToString()),
                        BlockNumber = TransformHextoDec(resultEth["blockNumber"]?.ToString()),
                        FromAddress = resultEth["from"]?.ToString(),
                        ToAdress = resultEth["to"]?.ToString()
                    };

                    bool success = await SaveTransactionHash(transaction);
                    if (!success)
                    {
                        return (null, "Error al guardar la transacción en la base de datos.");
                    }
                    return (transaction, null);
                };
            }
            catch (Exception ex) { 
             return (null, $"Error inesperado: {ex.Message}");
            }
            

        }


        private async Task<bool> SaveTransactionHash(Transaction transaction)
        {
            try
            {
                _context.Add(transaction);
                int result = await _context.SaveChangesAsync();
                return result>0;
            }
            catch {
                return false;
            }
           
        }

        private long TransformHextoDec(string  hexValue)
        {
            long value = long.Parse(hexValue.Substring(2), System.Globalization.NumberStyles.HexNumber);
            return value;
        }

        private long TranformHexToWei(string hexValue)
        {
            long value = long.Parse(hexValue.Substring(2), System.Globalization.NumberStyles.HexNumber);
            return value; 
        }


    }
}

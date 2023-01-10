using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using CalculatonDAL;
using CalculatonDAL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CalculationService;

public class CalculationFunction
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public APIGatewayProxyResponse CalculationFunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        string msgBody = string.Empty;

        try
        {
            if (request.QueryStringParameters != null && request.QueryStringParameters.Count > 0)
            {
                Dictionary<string, string> parameters = GetQueryParamValues(request.QueryStringParameters);
                using (var calcLogContext = new CalculationLogContext())
                {

                    CalculationItem item = new CalculationItem()
                    {
                        NumberOne = Convert.ToInt32(parameters["Number_One"]),
                        NumberTwo = Convert.ToInt32(parameters["Number_Two"]),
                        OperationCode = parameters["Operation_Code"]
                    };
                    item.Result_Value = PerformOperation(item);
                    msgBody = item.Result_Value.ToString();

                    calcLogContext.CalculationItems.Add(item);
                    calcLogContext.SaveChanges();
                }
            }
            else
            {
                msgBody = "Not enough input values";
            }    
        }
        catch(Exception ex)
        {
              msgBody = "Error occured " + ex.Message;
        }

        APIGatewayProxyResponse response = new APIGatewayProxyResponse();
        response.StatusCode = (int)HttpStatusCode.OK;
        response.Body = msgBody;

        return response;
    }

    private int PerformOperation(CalculationItem item) => item.OperationCode.Trim().ToUpper() switch
    {
        "ADD" => item.NumberOne + item.NumberTwo,
        "SUBS" => item.NumberOne - item.NumberTwo,
        "MULT" => item.NumberOne * item.NumberTwo,
        "DIVD" => item.NumberOne / item.NumberTwo,
        "EXP" => (int)Math.Pow((double)item.NumberOne, (double)item.NumberTwo),
        _ => 0
    };

    private Dictionary<string, string> GetQueryParamValues(IDictionary<string, string> queryParam)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        string? requestValue;
        
        queryParam.TryGetValue("Number_One", out requestValue);
        if (requestValue != null) result.Add("Number_One", requestValue);

        queryParam.TryGetValue("Number_Two", out requestValue);
        if (requestValue != null) result.Add("Number_Two", requestValue);

        queryParam.TryGetValue("Operation_Code", out requestValue);
        if (requestValue != null) result.Add("Operation_Code", requestValue);

        return result;
    }
}

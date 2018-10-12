using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//enumerator list for the type of order
internal enum order_type { IOC, GFD, INV };

internal struct order_table
{
    public int order_quantity { set; get; }
    public int order_price { set; get; }
    public order_type order_trade { set; get; }

}

class Solution
{

    //Order operation labels
    private const string BUY_LABEL = "BUY";
    private const string SELL_LABEL = "SELL";
    private const string CANCEL_LABEL = "CANCEL";
    private const string MODIFY_LABEL = "MODIFY";
    private const string PRINT_LABEL = "PRINT";


    //enumerator list for the type of operations
    internal enum operation_type { BUY, SELL, CANCEL, MODIFY, PRINT, INV };

    internal static Dictionary<string, order_table> buyTable = new Dictionary<string, order_table>();
    //internal static Dictionary<int, int> buyOrderTable = new Dictionary<int, int>();
    internal static Dictionary<int, int> printBuyOrderTable = new Dictionary<int, int>();

    //internal static Dictionary<string, Dictionary<int, int>> sellTable = new Dictionary<string, Dictionary<int, int>>();
    internal static Dictionary<string, order_table> sellTable = new Dictionary<string, order_table>();
    internal static Dictionary<int, int> printSellOrderTable = new Dictionary<int, int>();


    static void Main(String[] args)
    {
        /* Enter your code here. Read input using Console.ReadLine. Print output using Console.WriteLine. 
         * Your class should be named Solution */
        string[] stdInputArgumentsArray = new string[] { };



        //Reading the standard input arguments and spilt them based on spaces
        //Exmaple : BUY GFD 1000 10 order1
        //All the spilt argemtns are saved to an array named stdInputArgumentsArray

        //Console.Read();
        List<string> stdInput = new List<string>();

        string currentLine = " ";

        while ((currentLine = Console.ReadLine()) != null && currentLine != "")
        {
            stdInput.Add(currentLine);

        }
        //order_table _orderTable = new order_table();
        foreach (var line in stdInput)
        {
            stdInputArgumentsArray = line.Split(null);
            

            //Checks if the Trade statement is right
            if (isValidTrade(stdInputArgumentsArray[0], stdInputArgumentsArray))
            {
                switch (stdInputArgumentsArray[0])
                {
                    case BUY_LABEL:
                        {
                            //BUY Operation logic
                                try
                                {
                                    order_table _orderTable = new order_table();
                                    _orderTable = objectSetter(_orderTable, stdInputArgumentsArray);
                                    buyTable.Add((stdInputArgumentsArray[4]), _orderTable);
                                }
                                catch
                                {
                                    Console.WriteLine("Error?");
                                }
                            break;
                        }
                    case SELL_LABEL:
                        {
                            //SELL Operation logic
                                try
                                {
                                    order_table _orderTable = new order_table();
                                    _orderTable = objectSetter(_orderTable, stdInputArgumentsArray);
                                    sellTable.Add((stdInputArgumentsArray[4]), _orderTable);
                                }
                                catch
                                {
                                    Console.WriteLine("Repeated?");
                                }
                            break;
                        }
                    case CANCEL_LABEL:
                        {
                            //CANCEL Operation logic
                            //Removing Sell and BUY tabel entry
                            if (sellTable.ContainsKey(stdInputArgumentsArray[1]))
                            {
                                sellTable.Remove(stdInputArgumentsArray[1]);

                            }
                            else if (buyTable.ContainsKey(stdInputArgumentsArray[1]))
                            {
                                buyTable.Remove(stdInputArgumentsArray[1]);
                            }

                            break;
                        }
                    case MODIFY_LABEL:
                        {

                            //MODIFY Operation logic
                            if (stdInputArgumentsArray[2].Equals(SELL_LABEL))
                            {
                                if (sellTable.ContainsKey(stdInputArgumentsArray[1]))
                                {
                                    var temp = sellTable[stdInputArgumentsArray[1]];
                                    if (temp.order_trade.Equals(order_type.IOC) || temp.order_trade.Equals(order_type.INV)) break;
                                    order_table _orderTable = new order_table();
                                    _orderTable.order_price = Convert.ToInt32(stdInputArgumentsArray[3]);
                                    _orderTable.order_quantity = Convert.ToInt32(stdInputArgumentsArray[4]);
                                    _orderTable.order_trade = temp.order_trade;

                                    sellTable.Remove(stdInputArgumentsArray[1]);
                                    sellTable.Add(stdInputArgumentsArray[1], _orderTable);

                                }
                            }
                            if (stdInputArgumentsArray[2].Equals(BUY_LABEL))
                            {
                                if (buyTable.ContainsKey(stdInputArgumentsArray[1]))
                                {
                                    var temp = buyTable[stdInputArgumentsArray[1]];
                                    if (temp.order_trade.Equals(order_type.IOC) || temp.order_trade.Equals(order_type.INV)) break;
                                    order_table _orderTable = new order_table();
                                    _orderTable.order_price = Convert.ToInt32(stdInputArgumentsArray[3]);
                                    _orderTable.order_quantity = Convert.ToInt32(stdInputArgumentsArray[4]);
                                    _orderTable.order_trade = temp.order_trade;

                                    buyTable.Remove(stdInputArgumentsArray[1]);

                                    buyTable.Add(stdInputArgumentsArray[1], _orderTable);

                                    //buyTable.Add(stdInputArgumentsArray[1], _orderTable);
                                }
                            }
                            break;
                        }

                    default:
                        break;
                }
            }

        }

        try
        {
            //Let's Trade
            if (stdInputArgumentsArray[0].Equals(PRINT_LABEL) && isTrade())
            {
                //Console.Write("Here");
                //tempSellTable = sellTable;
                // try
                {
                    var totalNumberOfSellOrders = sellTable.Count;
                    //llTable.
                    int indexSellTable = 0;
                    // var tempSellTable = sellTable;
                    while (indexSellTable < totalNumberOfSellOrders)
                    {
                        foreach (var kvp1 in sellTable.ToArray())
                        {

                            // var currentSellOrderTrade = kvp1.Value.order_trade;
                            // Console.WriteLine("Let's Trade");

                            sellTrade(kvp1.Key.ToString());




                            //var flattenList = buyTable.SelectMany(x => x.Value.order_price);
                            //getAllBuysGreaterThanMine();
                        }
                        indexSellTable++;
                    }
                    foreach (var kvp1 in sellTable.ToArray())
                    {
                        if (kvp1.Value.order_trade.Equals(order_type.IOC))
                        {
                            sellTable.Remove(kvp1.Key);
                        }
                    }

                    foreach (var kvp1 in buyTable.ToArray())
                    {
                        if (kvp1.Value.order_trade.Equals(order_type.IOC))
                        {
                            buyTable.Remove(kvp1.Key);
                        }
                    }
                }
                // catch { }
                printOperation();
            }

            //check for SELL AND BUY Tables for suitable trades.
            else if (!isTrade() && stdInputArgumentsArray[0].Equals(PRINT_LABEL))
            {
                printOperation();
            }
            else if (isTrade())
            {
                var totalNumberOfSellOrders = sellTable.Count;
                //llTable.
                int indexSellTable = 0;
                // var tempSellTable = sellTable;
                while (indexSellTable < totalNumberOfSellOrders)
                {
                    foreach (var kvp1 in sellTable.ToArray())
                    {

                        // var currentSellOrderTrade = kvp1.Value.order_trade;
                        // Console.WriteLine("Let's Trade");

                        sellTrade(kvp1.Key.ToString());
                        //var flattenList = buyTable.SelectMany(x => x.Value.order_price);
                        //getAllBuysGreaterThanMine();
                    }
                    indexSellTable++;

                }

                foreach (var kvp1 in sellTable.ToArray())
                {
                    if (kvp1.Value.order_trade.Equals(order_type.IOC))
                    {
                        sellTable.Remove(kvp1.Key);
                    }
                }

                foreach (var kvp1 in buyTable.ToArray())
                {
                    if (kvp1.Value.order_trade.Equals(order_type.IOC))
                    {
                        buyTable.Remove(kvp1.Key);
                    }
                }


            }
            Console.ReadKey();
        }
        catch
        { Console.ReadKey(); }

        Console.ReadKey();
    }


    #region HELPER FUNCTIONS



    static void sellTrade(String Key)
    {

        
            //Console.WriteLine("Let's Trade");
            var sortedDict = from entry in buyTable orderby entry.Value.order_price descending select entry;

            int index = 0;
            foreach (var num in sortedDict)
            {
            if (!sellTable.ContainsKey(Key))
            { continue; }
                var obj = sellTable[Key];

                var currentSellOrderPrice = obj.order_price;//Convert.ToInt32(_orderTable.GetType().GetProperty("order_price").GetValue(_orderTable, null));

                //Console.WriteLine("CR: " + currentSellOrderPrice);
                var currentSellOrderType = obj.order_trade;//_orderTable.GetType().GetProperty("order_trade").GetValue(_orderTable, null);
                int currentSellOrderQunt = obj.order_quantity;//Se Convert.ToInt32(_orderTable.GetType().GetProperty("order_quantity").GetValue(_orderTable, null));
                var currentSellOrderID = Key;

                if (currentSellOrderPrice > num.Value.order_price && index.Equals(0)) return;

                var num_traded = returnLeastNumber(currentSellOrderQunt, num.Value.order_quantity);

                //editing SELL table
                var sellEntry = returnModifyArray(currentSellOrderID, operation_type.SELL, currentSellOrderPrice, num_traded, currentSellOrderType);
                var buyEntry = returnModifyArray(num.Key, operation_type.BUY, num.Value.order_price, num_traded, num.Value.order_trade);



                Console.WriteLine("TRADE {0} {1} {2} {3} {4} {5}", num.Key, num.Value.order_price,
                    num_traded, currentSellOrderID, currentSellOrderPrice, num_traded);

                modifyTable(sellEntry);
                modifyTable(buyEntry);


                if (currentSellOrderType.Equals(order_type.IOC))
                {
                    sellTable.Remove(Key);
                    // break;
                }
                if (num.Value.order_trade.Equals(order_type.IOC))
                {
                    buyTable.Remove(Key);
                    // break;
                }

                index++;
            }
            // printOperation();
            //return false;
        
    }

    static int returnLeastNumber(int a, int b)
    {
        //int min = 0;
        if (a <= b) return a;
        else return b; 

    }

    static string[] returnModifyArray(string OrderID, operation_type operation_Type, int OrderPrice ,int  num_traded, order_type OrderType)
    {
        //editing SELL table
        string[] tempArray = new string[6];
        tempArray[0] = operation_type.MODIFY.ToString();
        tempArray[1] = OrderID;
        tempArray[2] = operation_Type.ToString();
        tempArray[3] = OrderPrice.ToString();
        tempArray[4] = num_traded.ToString();
        tempArray[5] = OrderType.ToString();
        return tempArray;
    }

    static void printOperation()
    {
       
                  //PRINT Operation logic
            //Printing Sell operations
            generatePrintTable();

            Console.WriteLine("SELL:");
            foreach (var kvp1 in printSellOrderTable.Reverse())
            {

                //Console.WriteLine("Order ID = {0}", kvp1.Key);
                Console.WriteLine(kvp1.Key + " " + kvp1.Value);
            }
            //Printing BUY operations
            Console.WriteLine("BUY:");
            foreach (var kvp1 in printBuyOrderTable.Reverse())
            {
                //Console.WriteLine("Order ID = {0}", kvp1.Key);
                Console.WriteLine(kvp1.Key + " " + kvp1.Value);
            }
        
      

    }

    static void modifyTable(string[] stdInputArgumentsArray)
    {

        if (stdInputArgumentsArray[2].Equals(SELL_LABEL))
        {
            if (sellTable.ContainsKey(stdInputArgumentsArray[1]))
            {


                var temp = sellTable[stdInputArgumentsArray[1]];

                temp.order_price = Convert.ToInt32(stdInputArgumentsArray[3]);
                temp.order_quantity -= Convert.ToInt32(stdInputArgumentsArray[4]);


                if (temp.order_quantity <= 0)
                {
                    temp.order_quantity = 0;
                    sellTable.Remove(stdInputArgumentsArray[1]);
                }
                else
                    sellTable[stdInputArgumentsArray[1]] = temp;
            }


        }
        if (stdInputArgumentsArray[2].Equals(BUY_LABEL))
        {
            //foreach (var num in stdInputArgumentsArray)
            //{
            //    Console.WriteLine(" {0}", num);
            //}
            if (buyTable.ContainsKey(stdInputArgumentsArray[1]))
            {
                //buyTable.Remove(stdInputArgumentsArray[1]);
                var temp = buyTable[stdInputArgumentsArray[1]];

                temp.order_price = Convert.ToInt32(stdInputArgumentsArray[3]);
                temp.order_quantity -= Convert.ToInt32(stdInputArgumentsArray[4]);


                if (temp.order_quantity <= 0)
                {
                    temp.order_quantity = 0;
                    buyTable.Remove(stdInputArgumentsArray[1]);
                }
                else
                    buyTable[stdInputArgumentsArray[1]] = temp;
            }

        }
    }
        
    

    static order_table objectSetter(order_table _orderTable, string[] stdInputArgumentsArray)
    {
        try
        {
           
            if (Convert.ToInt32(stdInputArgumentsArray[2]) <= 0 || Convert.ToInt32(stdInputArgumentsArray[3]) <= 0) return _orderTable;
            _orderTable.order_price = Convert.ToInt32(stdInputArgumentsArray[2]); ;
            _orderTable.order_quantity = Convert.ToInt32(stdInputArgumentsArray[3]);
            if (getOrderType(stdInputArgumentsArray[1]).Equals(order_type.GFD))
            {
                //GFD Operation
                _orderTable.order_trade = order_type.GFD;
            }
            else if (getOrderType(stdInputArgumentsArray[1]).Equals(order_type.IOC))
            {
                //IOC operation
                _orderTable.order_trade = order_type.IOC;
            }
            else _orderTable.order_trade = order_type.INV;

            return _orderTable;
        }
        catch { return _orderTable; }
    }

    static void generatePrintTable()
    {
        foreach (var kvp1 in sellTable)
        {
            try
            {
                printSellOrderTable.Add(kvp1.Value.order_price, kvp1.Value.order_quantity);
            }
            catch
            {
                printSellOrderTable[kvp1.Value.order_price] += kvp1.Value.order_quantity;
            }
        }

        foreach (var kvp1 in buyTable)
        {
            try
            {
                printBuyOrderTable.Add(kvp1.Value.order_price, kvp1.Value.order_quantity);
            }
            catch
            {
                printBuyOrderTable[kvp1.Value.order_price] += kvp1.Value.order_quantity;
            }
        }

    }

    static order_type getOrderType(string current_order)
    {


        if (current_order.Equals("GFD"))
        {
            //GFD operation
            return order_type.GFD;

        }
        else if (current_order.Equals("IOC"))
        {
            //IOC operation
            return order_type.IOC;
        }
        else return order_type.INV;
    }

    static bool isValidTrade(string operation, string[] operation_statement_array)
    {
        if (operation.Equals(operation_type.BUY.ToString())
            || operation.Equals(operation_type.SELL.ToString())
            || operation.Equals(operation_type.MODIFY.ToString()))
        {
            //return true if arguments passed are 5
            if (operation_statement_array.Length.Equals(5)) return true;
        }
        else if (operation.Equals(operation_type.CANCEL.ToString()))
        {
            //return true if arguments passed are 3
            if (operation_statement_array.Length.Equals(2)) return true;
        }
        else if (operation.Equals(operation_type.PRINT.ToString()))
        {
            //return true if arguments passed are 3
            if (operation_statement_array.Length.Equals(1)) return true;
        }


        return false;
    }

    static bool isTrade()
    {
        if (buyTable.Count.Equals(0) || sellTable.Count.Equals(0)) return false;

        if (!buyTable.Count.Equals(0) && !sellTable.Count.Equals(0))
        {
            //Console.WriteLine("Let's Trade");
            return true;
        }
        return false;
    }
    
}
#endregion
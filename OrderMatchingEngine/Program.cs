    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;


    public struct order_table
    {
        public int order_quantity { set; get; }
        public int order_price { set; get; }
    }   

    class Solution
    {

        //Order operation labels
        private const string BUY_LABEL = "BUY";
        private const string SELL_LABEL = "SELL";
        private const string CANCEL_LABEL = "CANCEL";
        private const string MODIFY_LABEL = "MODIFY";
        private const string PRINT_LABEL = "PRINT";

        //enumerator list for the type of order
        internal enum order_type { IOC, GFD, INV };
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
        
            foreach (var line in stdInput)
            {
                stdInputArgumentsArray = line.Split(null);
                order_table _orderTable = new order_table();

                //Checks if the Trade statement is right
                if (isValidTrade(stdInputArgumentsArray[0], stdInputArgumentsArray))
                    {
                        switch (stdInputArgumentsArray[0])
                        {
                            case BUY_LABEL:
                                {
                                //BUY Operation logic
                           
                                    if (getOrderType(stdInputArgumentsArray[1]).Equals(order_type.GFD))
                                    {
                                        //GFD Operation
                                        // Console.WriteLine("BUY GFD");
                                        try
                                        {
                                        //buyOrderTable.Add(Convert.ToInt32(stdInputArgumentsArray[2]),
                                        //    Convert.ToInt32(stdInputArgumentsArray[3]));
                                            _orderTable.order_price = Convert.ToInt32(stdInputArgumentsArray[2]); ;
                                            _orderTable.order_quantity =  Convert.ToInt32(stdInputArgumentsArray[3]);
                                            buyTable.Add((stdInputArgumentsArray[4]), _orderTable);

                                  
                                        }
                                        catch
                                        {
                                             Console.WriteLine("Repeated?");
                                            //buyOrderTable[Convert.ToInt32(stdInputArgumentsArray[2])] +=
                                              //  Convert.ToInt32(stdInputArgumentsArray[3]);
                                        }

                                    }
                                    else if ((getOrderType(stdInputArgumentsArray[1]).Equals(order_type.IOC)))
                                    {
                                        //IOC Operation

                                        // Console.WriteLine("BUY IOC");

                                    }
                                    else
                                    {
                                        //Invlaid operation
                                        return;
                                    }


                                    break;
                                }
                            case SELL_LABEL:
                                {
                                    //SELL Operation logic

                                    if (getOrderType(stdInputArgumentsArray[1]).Equals(order_type.GFD))
                                    {
                                        //GFD Operation
                                        // Console.WriteLine("SELL GFD");
                                        try
                                        {
                                            _orderTable.order_price = Convert.ToInt32(stdInputArgumentsArray[2]); ;
                                            _orderTable.order_quantity = Convert.ToInt32(stdInputArgumentsArray[3]);
                                            sellTable.Add((stdInputArgumentsArray[4]), _orderTable);


                                        }
                                        catch
                                        {
                                            Console.WriteLine("Repeated?");
                                            //buyOrderTable[Convert.ToInt32(stdInputArgumentsArray[2])] +=
                                            //  Convert.ToInt32(stdInputArgumentsArray[3]);
                                        }

                            }
                                    else if ((getOrderType(stdInputArgumentsArray[1]).Equals(order_type.IOC)))
                                    {
                                        //IOC Operation

                                        // Console.WriteLine("BUY IOC");

                                    }
                                    else
                                    {
                                        //Invlaid operation
                                        return;
                                    }


                                    break;
                                }
                            case CANCEL_LABEL:
                                {
                                //CANCEL Operation logic
                                // Console.WriteLine("removing entry" + stdInputArgumentsArray[1]);
                                if (sellTable.ContainsKey(stdInputArgumentsArray[1]))
                                {
                                    try
                                    {
                                        var tempQuantity = sellTable[stdInputArgumentsArray[1]].order_quantity;
                                        var tempPrice = sellTable[stdInputArgumentsArray[1]].order_price;
                                        printSellOrderTable[tempPrice] -= tempQuantity;
                                        sellTable.Remove(stdInputArgumentsArray[1]);
                                    }
                                    catch { sellTable.Remove(stdInputArgumentsArray[1]); }

                                }
                                else if (buyTable.ContainsKey(stdInputArgumentsArray[1]))
                                {
                                    try
                                    {
                                        var tempQuantity = buyTable[stdInputArgumentsArray[1]].order_quantity;
                                        var tempPrice = buyTable[stdInputArgumentsArray[1]].order_price;
                                        printBuyOrderTable[tempPrice] -= tempQuantity;
                                        buyTable.Remove(stdInputArgumentsArray[1]);
                                    }
                                    catch { buyTable.Remove(stdInputArgumentsArray[1]); }
                                }
                                    //else return;
                                break;
                                }
                            case MODIFY_LABEL:
                                {
                                    //MODIFY Operation logic
                                    break;
                                }
                   
                            default:
                                break;

                        }
                    }

                }

                if (stdInputArgumentsArray[0].Equals(PRINT_LABEL))
                {

                    printOperation();
            
                }


                Console.ReadKey();
        }


    #region HELPER FUNCTIONS
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
    }
#endregion
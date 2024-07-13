# ** API Documantation **
## **How the system works**
Skyblock bazzar tracker connecting to the hypixel api and pulling information about the current bazzar prices
after that the user adding the products he would like to track
the software getting the current price from what it already picked from the [API](#how-to-use-api) and showing to the user chart that represent the margin between prices per product and another angular chart showing the whole profit or lose
<br/>

***

## **How to use API** 
*Currently the API class only giving you access to the bazzar prices request All the inforamtion is being pulled from [hypixel API skyblock Section](https://api.hypixel.net/#tag/SkyBlock)*
* For the API there is class called: [***Api_Connector.cs***](../Skyblock%20Bazzar%20Tracker/Api_Connector.cs)
    * First you need to create object of type Api_connector
    * Than call the task function ***GetCurrent_bazzar()*** The Task getting all the current orders in bazzar data and inserting it into dictonary ([API Documantation](https://api.hypixel.net/#tag/SkyBlock/paths/~1v2~1skyblock~1bazaar/get))
    * The task return is Dictonary of all the items where the keys is the item id and the values is object of type **[Products_api_info](#class-products_api_info)**
<br/>

***

## **Class [Products_api_info](../Skyblock%20Bazzar%20Tracker/Products_api_info.cs)**
*Class that holding the values of the items we got from the API*
* **Array<[sell_summary](#sell_summery_class)> -** array of type sell summary
* **[Quick_status_class](#quick_status_class) quick_status -** is a computed summary of the live state of the product (used for advanced mode view in the bazaar:
* **string product_id -** The product ID
    ### ***Sell_summery_class***
    * **int amount -** The amount of sell orders at this price
    * **double pricePerUnit -** Price Per unit
    * **int orders -** I trully have no idea what orders mean
 
    ### ***Quick_status_class***
    * **string productId    -** Product ID
    * **double sellPrice    -** weighted average of the top 2% of orders by volume
    * **int sellVolume      -** are the sum of item amounts in all orders 
    * **int sellMovingWeek  -** is the historic transacted volume from last 7d + live state
    * **int sellOrders      -** the count of active orders
    * **double buyPrice     -** weighted average of the top 2% of orders by volume
    * **int buyVolume       -** are the sum of item amounts in all orders
    * **int buyMovingWeek   -** is the historic transacted volume from last 7d + live state
    * **int buyOrders       -** the count of active orders

<br/>

***

## **Class [Api_Connector](../Skyblock%20Bazzar%20Tracker/Api_Connector.cs)**
*Class that will associate with the API communication*
* **Constructor Api_Connector() -** Function that currently doing nothing but made for using the API 
* **GetCurrent_bazzar() -** Async function that getting from the API Response in ***[Hypixel_Api_answer](../Skyblock%20Bazzar%20Tracker/Hypixel_Api_answer.cs) ***and extracting the products out of the response and returning dictonary of all the products and their data

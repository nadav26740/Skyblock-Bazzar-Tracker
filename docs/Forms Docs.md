# **Forms Documantation**

## ***Dependencies***
> * **WPF (.NET Core 6)**
>
> ### **Nuget Packages**
>> * LiveCharts.Wpf
>> * LiveCharts.Wpf.NetCore3
>> * Newtonsoft.Json


## Main Window
> Main using LiveCharts 0.9.8v for the graphs
>> The main Window initilizing at the begining 3 candles (Buy price, Current Price, Margin) <br>
>> after the load the interval will be initilized that every 30 seconds will run the function Reload_function <br>
>> and also will call the window [ImportFromFile()](#load-window) that will add trackers from file writin in json
> ### Reload_function
>> Running async LoadProducts_current_values that will get from the api all <br> the current prices and than present them in the window
> ### Update_Charts
>> Loading all the data into the window
> ### AutoComplete_fields
>> *Getting called everytime you fill text into the id*
>> * Automatic complete all the fields based on the id if exists
> ### Add_order_clicked
>> Adding all the order in the fields to trackers graph
> ### Remove_track_clicked
>> Removing track based on the last track that has been pressed
> ### CartesianChart_DataClick
> Main using LiveCharts.WPF for the graphs
> ### Main Window is built by different Components:
> #### Add New Tracker:
>> Has Item id box that will open searchbox That will run the function:
>>```c#
>> private void Item_Id_Textbox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
>> // function that will automaticly fill the id based on the searched product
>> ```
>> After You Feel the ID all the other fields getting automaticlly field based on the current Data
>> * Item Buy Price - The Current insta sell price
>> * Item name - id without the '_'
>
> ####  The Gauge:
>> Represents the current profit by Current sell - buy price in precentage
>
> #### Remove Box
>> By pressing one of the graph you automaticlly will add this track to remove box
>
> ### Auto Data Gather
>> There is an automatic Timer Called Automatic_Reload_Timer that every 30 sec will gather data from the API by running the function
>> ```cs
>> public async void LoadProducts_current_values()
>> {
>> // ...
>> // Function the present the data on the graphs
>>      Update_Charts();
>> }
>> ```

## Save Window
> ### SaveBtn_Click
>> If save button has been clicked is showing dialog file of windows and <br> saving the data in json format at the file that has been choosed


## Load Window
> ### LoadBtn_Click
>> opening file dialog and reading all the data in this file from json <br> format and than returning the data to the main window

## Search Window
> ### Search_algo
>> * Everytime key pressed on the search box the function event search_box_TextChanged called <br> and if field isn't empty calling Search_algo
> ### Search_algo
>> * the function loading the first 8 Ids of items that contain the searched text
> ### List_view_MouseDoubleClick
>> * The pressed list item is getting loaded into the fields of the main window
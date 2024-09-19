# **Forms Documantation**

## ***Dependencies***
> * **WPF (.NET Core 6)**
>
> ### **Nuget Packages**
>> * LiveCharts.Wpf
>> * LiveCharts.Wpf.NetCore3
>> * Newtonsoft.Json


## Main Window
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

## Load Window

## Search Window

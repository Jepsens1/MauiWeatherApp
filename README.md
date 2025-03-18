# Weather App built in .NET Maui using OpenWeatherMap API endpoints

## Description
Very basic app that allows you to type in a city name or use current geolocation to get both current and 5 day / 3 hour forecasts

It uses openWeatherMap API endpoints to get JSON data that it then deserialized using Newtonsoft.JSON
> [!NOTE]
> JSON data from forecast endpoint each timestamp is UNIX and is converted to DateTime and then is being grouped using LINQ orderby and groupby methods

The idea behind this project was to get a deeper understanding of working with views `.xaml` converters and viewmodels using `CommunityToolKit.MVVM`
So the focus for the `Logic` project was bare minimum for getting data and generic error handling. 

## Installation Guide

```bash
git clone https://github.com/Jepsens1/MauiWeatherApp.git
```
Create your own `appsettings.json` and add a section called `APIKEY` and paste in your api key from OpenWeatherMap

# Screenshots
## Overview 
![billede](https://github.com/user-attachments/assets/7f5b29fd-00d0-4be5-bc9f-74332c7f1d9c)

## Expandable tabs for each timestamp
![billede](https://github.com/user-attachments/assets/74c9b43d-fa7f-4900-a9e3-4efecdb59332)

![billede](https://github.com/user-attachments/assets/b2438420-a795-4238-a033-4f0b6ff78d7c)



# FroniusReader
Window Application for reading data via Fronius Solar API 


The application connects to a Fronius Inverter (e.g. Fronius Symo 8.2-3-m) and retrieves the data stored inside the internal time series database.
It can be used to analyze the voltage and currents of the single strings in order to find out if the system is running perfectly.

![Alt text](doc/FroniusReader.png?raw=true "Fronius Reader") 
**Fig. 1:** Main Screen

# Warning
The application was coded in a hurry and is still under construction. 
Note that you can normally not damage the inverter by just reading data.
Further information is available under 
https://www.fronius.com/en/solar-energy/installers-partners/technical-data/all-products/system-monitoring/open-interfaces/fronius-solar-api-json-


# Usage
## GetArchiveData
Reads historical data stored in the inverter.
- First you must enter the IP or URL into the top left input field. In my case the inverter is named fronius.fritz.box.
- Enter the number of hours from now into the past to specifiy the range of the data.
- Click on the button GetArchiveData to read out the values

## GetInverterRealtimeData
Reads the current values from the inverter.
- Click the button GetInverterRealtimeData and the data is displayed next to the button.

## GetMeterRealtimeData
Reads the current values from the smart meter (e.g. Fronius Smart Meter 63a-3) if available.
- Click the button GetMeterRealtimeData and the data is displayed next to the button.


# Thanks for your donation
If you want to support this free project. Any help is welcome. You can donate by clicking one of the following links:
<a target="blank" href="https://blockchain.com/btc/payment_request?address=1PBi7BoZ1mBLQx4ePbwh1MVoK2RaoiDsp5"><img src="https://img.shields.io/badge/Donate-Bitcoin-green.svg"/></a>
<a target="blank" href="https://www.paypal.me/windkh"><img src="https://img.shields.io/badge/Donate-PayPal-blue.svg"/></a>

<a href="https://www.buymeacoffee.com/windka" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>



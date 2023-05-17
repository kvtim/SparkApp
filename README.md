## How to run

Clone this repository:

```bash
git clone https://github.com/kvtim/SparkApp.git
```
Go to directory with SparkApp.dll this repository:

```bash
cd SparkApp\bin\Debug\net7.0
```

Run this command:

```bash
spark-submit --class org.apache.spark.deploy.dotnet.DotnetRunner --master local microsoft-spark-3-2_2.12-2.1.1.jar dotnet SparkApp.dll ../../../../input.txt
```
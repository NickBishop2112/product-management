import { ChartData, Chart as ChartJS, ChartOptions, registerables } from "chart.js";
import { groupBy, sumBy } from "lodash";
import { useEffect, useState } from "react";
import { Bar } from "react-chartjs-2";
import { getProductDetails } from "../services/ProductsService.";

ChartJS.register(...registerables);

export const StockTotals = () => {

    const [chartData, setChartData] = useState<ChartData<"bar", number[], string>>({
        labels: [],
        datasets: []
    });
    
    useEffect(() => {
            const products = getProductDetails();  

            products.then((data) => {

                const groupedProducts = groupBy(data, "category");
                const stockTotals = Object.entries(groupedProducts).map(([category, items]) => ({
                    category,
                    totalStock: sumBy(items, "stockQuantity"),
                  }));

                const categories = stockTotals.map((stockTotal) => stockTotal.category);
                const totals = stockTotals.map((stockTotal) => stockTotal.totalStock);
                
                console.log(categories);
                console.log(totals);
                const x: ChartData<"bar", number[], string> = {
                    labels: categories,
                    datasets: [
                        {
                            label: "Stock Totals",
                            data: totals,
                            backgroundColor: "rgba(75, 192, 192, 0.2)",
                            borderColor: "rgba(75, 192, 192, 1)",
                            borderWidth: 1
                        }
                    ]}
                
                setChartData(x);                  
            });
        }, []); 

        const options: ChartOptions<"bar"> = {
            responsive: true,
            plugins: {
                legend: {
                    display: true, 
                    position: "top" 
                },
                tooltip: {
                    enabled: true 
                }
            },
            scales: {
                x: {      
                    ticks: {
                        autoSkip: false, 
                        maxRotation: 45, 
                        minRotation: 45  
                    },
                    title: {
                        display: true,   
                        text: "Categories" 
                    }                             
                },
                y: {
                    beginAtZero: true,
                    ticks: {
                        stepSize: 10, 
                        callback: (value) => value.toString()
                    },
                    title: {
                        display: true,
                        text: "Stock Total"
                    }
                }
            }};

    return (
      <div style={{ width: "800px", height: "400px", margin: "0 auto" }}>
        <h3>Stock Totals</h3>
        <div>
            <Bar data={chartData} options={options} />                  
        </div>
      </div>
    );
}
import { AllCommunityModule, ModuleRegistry } from 'ag-grid-community';
import "ag-grid-community/styles/ag-theme-alpine.css";
import { AgGridReact } from "ag-grid-react";
import { useEffect, useState } from 'react';
import { Product } from '../model/Product';
import { getProductDetails } from '../services/ProductsService.';
ModuleRegistry.registerModules([AllCommunityModule]);


export const ProductDetails = () => {

    const [rowData, setRowData] = useState<Product[]>([]); 
    
    const columnDefs: { field: keyof Product; width?: number; headerName:string;filter: boolean}[] = [
        { field: "productCode", width: 100, headerName: "Code", filter: true },
        { field: "category", width: 100, headerName: "Category", filter: false},
        { field: "name", width: 200, headerName: "Name", filter: true },
        { field: "price", width: 80, headerName: "Price", filter: false },
        { field: "stockQuantity", width: 90, headerName: "Qty",filter: false },
        { field: "dateAdded", width: 200, headerName: "Date Add.",filter: false }
    ];

    useEffect(() => {
        const products = getProductDetails();        
        products.then((data) => {
            setRowData(data);
        });
    }, []); 
    
    return (
        <div>
            <h3>Product Details</h3>
            <div
                style={{
                    display: "flex",
                    justifyContent: "center", 
                    alignItems: "flex-start", 
                    padding: "20px",
                }}>
                <div className="ag-theme-alpine" style={{ height: "400px", width: "800px" }}>            
                    <AgGridReact
                        columnDefs={columnDefs}
                        rowData={rowData}    
                        defaultColDef={{ sortable: true, filter: true }}                
                    />
                </div>
            </div>
        </div>
    )
}
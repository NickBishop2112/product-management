import axios from "axios";
import { Product } from "../model/Product";

const apiClient = axios.create({
    baseURL: "http://localhost:5002/api/products",
    timeout: 10000, 
    headers: {
      "Content-Type": "application/json",
    },
})

export const getProductDetails = async () => {
    try {
        const response = await apiClient.get<Product[]>("/details")        
        return response.data
    } catch (error) {
        console.error("Error fetching products: ", error)
        return []
    }
}
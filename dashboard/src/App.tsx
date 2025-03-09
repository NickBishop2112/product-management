import './App.css';
import { ProductDetails } from './components/ProductDetails';

import { StockTotals } from './components/StockTotals';

function App() {
  return (
    <div className="App">
        <h2>Products Management Dashboard</h2>        
        <ProductDetails />                     
        <StockTotals />  
    </div>
  );
}

export default App;

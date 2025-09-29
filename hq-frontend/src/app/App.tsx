import './App.css';
import {Router} from "./router"
import { BrowserRouter } from 'react-router-dom';
import { AuthProvider } from './AuthProvider';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <AuthProvider>
          <Router/>
        </AuthProvider>
      </BrowserRouter>
    </div>
  );
}

export default App;

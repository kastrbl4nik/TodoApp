import {
    BrowserRouter as Router,
    Route,
    Routes,
} from "react-router-dom";
import Home from "./Home";
import Login from "./Login";
import Register from "./Register";

function App() {
    return (
        <Router>
            <Routes>
                <Route path='*' element={<Register />} />
                <Route path="/" element={<Register />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/:username/home" element={<Home />} />
            </Routes>
        </Router>
    );
}

export default App;

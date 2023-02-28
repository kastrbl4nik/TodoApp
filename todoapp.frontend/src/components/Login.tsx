import axios from "axios";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import styles from "./Authorization.module.css";

function Login() {
    const url = 'https://localhost:44307/api';
    const navigate = useNavigate();

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');
    
    function login() {
        setLoading(true);
        axios.post(`${url}/login`, {username: username, password: password})
        .then((response) => {
            const token = response.data
            localStorage.setItem('token', token);
            navigate(`/${username}/home`);
        })
        .catch(err => {
            setError(err.response.data);
        })
        .finally(() => {
            setLoading(false);
        })
    }
    return(
        <div className={styles.container}>
            <h1>Welcome back!</h1>
            <div className={'box ' + styles.inputBox}>
                {error ? <div className={'box ' + styles.errorBox}>{error}</div> : ''}
                <input type="text" placeholder="Username" onChange={(event) => setUsername(event.target.value)}/>
                <input type="password" placeholder="Password" onChange={(event) => setPassword(event.target.value)}/>
                {loading ? <div className={styles.loader}/> : <button onClick={() => login()} type="submit">Sign in</button>}
                <small>Don't have an account yet? <Link to="/register">Sing up</Link></small>
            </div>
        </div>
    );
}

export default Login;
import axios from "axios";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import styles from "./Authorization.module.css";

function Register() {
    const url = 'https://localhost:44307/api';
    const navigate = useNavigate();
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    function signUp() {
        setLoading(true);
        axios.post(`${url}/register`, {username: username, password: password})
        .then((response) => {
            return axios.post(`${url}/login`, {username: username, password: password});
        })
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
            <h1>Welcome!</h1>
            <div className={'box ' + styles.inputBox}>
                {error ? <div className={'box ' + styles.errorBox}>{error}</div> : ''}
                <input type="text" placeholder="Username" onChange={(event) => setUsername(event.target.value)}/>
                <input type="password" placeholder="Password" onChange={(event) => setPassword(event.target.value)}/>
                {loading ? <div className={styles.loader}/> : <button onClick={() => signUp()} type="submit">Sign up</button>}
                <small>Already have an account? <Link to="/login">Sing in</Link></small>
            </div>
        </div>
    );
}

export default Register;
import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

interface User {
    username?: string,
}

interface TodoList {
    title: string,
    description: string,
    hidden: boolean,
}

interface Todo {
    title: string,
    description: string,
    completed: boolean,
}

function Home() {
    const url = 'https://localhost:44307/api';
    const { username } = useParams();
    const [user, setUser] = useState<User>();
    const [todoLists, setTodoLists] = useState<TodoList[]>();
    useEffect(() => {
        axios.get<User>(`${url}/users/${username}`).then(response => {
            setUser(response.data);
        });
        axios.get<TodoList[]>(`${url}/users/${username}/todoLists`).then(response => {
            setTodoLists(response.data);
        });
    }, [username]);
    return (
        <div className="container">
            <header className="box">
                <button className="burger">
                    <div />
                    <div />
                    <div />
                </button>
                <input type="search"/>
                <image className="avatar"/>
            </header>
            <div className="tagContainer">
                <button className="tag">Primary</button>
                <button className="tag tagActive">All</button>
                <button className="tag">Archieved</button>
            </div>
            <main className="todoListsContainer">
                <aside>
                    {todoLists?.map(todoList => {
                        return (
                            <button className="box" onClick={() => {}}>
                                {todoList.title}
                            </button>
                        );
                    })}
                </aside>
                <section className="box todoListSelected">
                    <h1>ToDo List #1</h1>
                </section>
            </main>
            <footer>
                {user?.username}
            </footer>
        </div>
    );
}

export default Home;
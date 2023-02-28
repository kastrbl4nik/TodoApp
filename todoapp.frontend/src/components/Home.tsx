import axios, { AxiosResponse } from "axios";
import { useCallback, useEffect, useState } from "react";
import { useParams } from "react-router-dom"
import { Todo, TodoList } from "../models/models";

import styles from './Home.module.css';
import Header from "./Header";
import TodoListSmall from "./TodoListSmall";
import TodoListBig from "./TodoListBig";
import TodoView, { TodoNew } from "./TodoView";

interface Filter {
    hidden?: boolean,
    nameLike?: string,
}

function Home() {
    const { username } = useParams();
    const api = axios.create({
        baseURL: 'https://localhost:44307/api',
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
    });

    const [filter, setFilter] = useState<Filter>({});
    const [todoLists, setTodoLists] = useState<TodoList[]>([]);
    const [todoList, setTodoList] = useState<TodoList>();
    const [todos, setTodos] = useState<Todo[]>([]);

    useEffect(() => {
        fetchTodoLists(filter)
        .then(response => {
            setTodoLists(response.data);
            if(!todoList)
                setTodoList(response.data.at(0));
        })
    }, [filter]);

    useEffect(() => {
        if(!todoList) return;
        fetchTodos().then((response) => {
            setTodos(response.data);
        })
    }, [todoList])

    const handleDeleteTodoList = useCallback(() => {
        deleteTodoList().then(() => {
            fetchTodoLists(filter)
            .then(response => {
                setTodoLists(response.data);
                setTodoList(response.data.at(0));
            })  
        });
    }, [todoLists, deleteTodoList]);

    const handleCreateTodoList = useCallback((newTodoList: TodoList) => {
        if(filter.hidden)
            newTodoList.hidden = filter.hidden;
        createTodoList(newTodoList).then((response) => {
            setTodoList(response.data);
            fetchTodoLists(filter)
            .then(response => {
                setTodoLists(response.data);
            })  
        });
    }, [filter, createTodoList]);

    const handleUpdateTodoList = useCallback((updatedTodoList: TodoList) => {
        if (!todoList) return;
        updateTodoList(updatedTodoList).then((response) => {
            setTodoList(response.data);
            fetchTodoLists(filter)
            .then(response => {
                setTodoLists(response.data);
            })  
        });
    }, [todoList, updateTodoList]);

    function fetchTodoLists(filter: Filter): Promise<AxiosResponse<TodoList[], any>> {
        let params = '';
      
        if (filter.hidden !== undefined) {
          params += `hidden=${filter.hidden}&`;
        }
      
        if (filter.nameLike !== undefined) {
          params += `nameLike=${filter.nameLike}&`;
        }
        
        params = params.replace(/&$/, '');
        const url = `users/${username}/todoLists${params ? `?${params}` : ''}`;
      
        return api.get<TodoList[]>(url);
      }

    function fetchTodos(): Promise<AxiosResponse<Todo[], any>> {
        if(!todoList) return Promise.reject();
        return api.get<Todo[]>(`users/${username}/todoLists/${todoList.id}/todos`);
    }

    function deleteTodoList(): Promise<AxiosResponse<TodoList, any>> {
        if(!todoList) return Promise.reject();
        return api.delete<TodoList>(`users/${username}/todoLists/${todoList.id}`);
    }

    function createTodoList(todoList: TodoList): Promise<AxiosResponse<TodoList, any>> {
        if(!todoList) return Promise.reject();
        return api.post<TodoList>(`users/${username}/todoLists`, todoList);
    }

    function updateTodoList(todoList: TodoList): Promise<AxiosResponse<TodoList, any>> {
        if(!todoList) return Promise.reject();
        return api.put<TodoList>(`users/${username}/todoLists/${todoList.id}`, todoList);
    }

    function createTodo(todo: Todo): Promise<AxiosResponse<Todo, any>> {
        if(!todoList) return Promise.reject();
        return api.post<Todo>(`users/${username}/todoLists/${todoList.id}/todos`, todo);
    }

    function deleteTodo(todo: Todo): Promise<AxiosResponse<Todo, any>> {
        if(!todoList) return Promise.reject();
        return api.delete<Todo>(`users/${username}/todoLists/${todoList.id}/todos/${todo.id}`);
    }

    function updateTodo(todo: Todo): Promise<AxiosResponse<Todo, any>> {
        if(!todoList) return Promise.reject();
        return api.put<Todo>(`users/${username}/todoLists/${todoList.id}/todos/${todo.id}`, todo);
    }

    const handleDeleteTodo = useCallback((todo: Todo) => {
        deleteTodo(todo).then(() => {
            fetchTodos().then(response => {
                setTodos(response.data);
            })
        });
    }, [todos, deleteTodo]);

    const handleCreateTodo = useCallback((todo: Todo) => {
        createTodo(todo).then(() => {
            fetchTodos().then(response => {
                setTodos(response.data);
            })
        });
    }, [todos, createTodo]);

    const handleUpdateTodo = useCallback((todo: Todo) => {
        console.log(todo);
        updateTodo(todo).then(() => {
            fetchTodos().then(response => {
                setTodos(response.data);
            })
        });
    }, [todos, updateTodo]);

    return (
        <div className={styles.container}>
            <Header onSearch={(value) => setFilter({...filter, nameLike: value})} />
            <div className={styles.tagContainer}>
                <button className={`${styles.tag} ${filter.hidden === false ? styles.tagActive : ''}`} onClick={() => setFilter({hidden: false})}>Primary</button>
                <button className={`${styles.tag} ${filter.hidden === undefined ? styles.tagActive : ''}`} onClick={() => setFilter({})}>All</button>
                <button className={`${styles.tag} ${filter.hidden === true ? styles.tagActive : ''}`} onClick={() => setFilter({hidden: true})}>Archieved</button>
            </div>
            <main className={styles.mainContainer}>
                <aside>
                    {todoLists.map((todoList, key) => {
                        return (
                            <TodoListSmall todoList={todoList} key={key} onClick={() => setTodoList(todoList)}/>
                        );
                    })}
                    <TodoListSmall onClick={() => handleCreateTodoList({title: "New List", hidden: false})}/>
                </aside>
                {todoList ? 
                <section>
                    <TodoListBig todoList={todoList} setTodoList={setTodoList} 
                        onDelete={() => handleDeleteTodoList()}
                        onUpdate={handleUpdateTodoList}>
                        {todos.map((todo, index) => {
                            return <TodoView key={index} 
                                todo={todos[index]}  
                                setTodo={(updatedTodo: Todo) => {
                                    setTodos(prevTodos => {
                                        const newTodos = [...prevTodos];
                                        newTodos[index] = updatedTodo;
                                        return newTodos;
                                    });
                                }}
                                onUpdate={handleUpdateTodo}
                                onDelete={handleDeleteTodo}/>
                        })}
                        {<TodoNew onCreate={() => handleCreateTodo({title: "New Todo", completed: false})}/>}
                    </TodoListBig> 
                </section>
                : ''}
            </main>
            <footer>
            </footer>
        </div>
    );
}

export default Home;
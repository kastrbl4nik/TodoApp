import { Dispatch, SetStateAction, useState } from "react";
import { BsPlusCircle, BsXCircle } from "react-icons/bs";
import { Todo } from "../models/models";
import styles from './TodoView.module.css';

interface Props {
    todo: Todo,
    setTodo: (todo: Todo) => void;
    onDelete: (todo: Todo) => void,
    onUpdate: (todo: Todo) => void,
}

function TodoView({ todo, setTodo, onDelete, onUpdate }: Props) {
    return (
        <div className={styles.todo}>
            <input type="checkbox" className={styles.checkbox} id={todo.id} checked={todo.completed} 
                onChange={(e) => {
                    const updatedTodo = { ...todo, completed: e.target.checked };
                    onUpdate(updatedTodo);
                }} />
            <label htmlFor={todo.id} />
            <input  type="text" value={todo.title} 
                onChange={(e) => setTodo({ ...todo, title: e.target.value })}
                onBlur={(e) => onUpdate({...todo, title: e.target.value})} />
            <input type="date" defaultValue={todo.dueDate?.substring(0,10)} 
                onChange={(e) => setTodo({ ...todo, dueDate: e.target.value })}
                onBlur={(e) => onUpdate({...todo, dueDate: e.target.value})} />
            <button className={styles.deleteTodo}
                onClick={() => onDelete(todo)}><BsXCircle /></button>
        </div>
    );
}

export function TodoNew({ onCreate }: { onCreate: () => void }) {
    return (
        <div className={styles.todo}>
            <button className={styles.createTodo} onClick={() => onCreate()}>
                <BsPlusCircle /> <span>Create new todo</span>
            </button>
        </div>
    );
}

export default TodoView;
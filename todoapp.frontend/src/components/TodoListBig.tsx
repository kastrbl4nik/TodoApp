import styles from './TodoListBig.module.css';
import { TodoList } from '../models/models';
import { BsArchive, BsEye, BsEyeSlash, BsPlusCircle, BsXCircle } from 'react-icons/bs';
import { Dispatch, ReactNode, SetStateAction } from 'react';

interface Props {
    todoList: TodoList,
    setTodoList: Dispatch<SetStateAction<TodoList | undefined>>,
    onDelete: () => any,
    onUpdate: (updatedTodoList: TodoList) => any,
    children?: ReactNode,
}

function TodoListBig({todoList, setTodoList, onDelete, onUpdate, children}: Props) {
    return (
        <div className={'box ' + styles.container}>
            <div className={styles.icon}>
                {todoList.title.substring(0, 1)}
            </div>
            <div className={styles.content}>
                <div>
                    <div className={styles.title}>
                        <input type="text" value={todoList.title} 
                            onChange={(e) => setTodoList({ ...todoList, title: e.target.value })} 
                            onBlur={(e) => onUpdate({...todoList, title: e.target.value})}/>
                        <div className={styles.leftSide}>
                            <button onClick={() => onUpdate({...todoList, hidden: !todoList.hidden})}>
                                {todoList.hidden ?  <BsEyeSlash /> : <BsEye />}
                            </button>
                            <button onClick={() => onDelete()}><BsXCircle /></button>
                        </div>
                    </div>
                    <input className={styles.description} type="text" value={todoList.description ?? ''} 
                        onChange={(e) => setTodoList({ ...todoList, description: e.target.value })}
                        onBlur={(e) => onUpdate({...todoList, description: e.target.value})}/>
                </div>
                <div className={styles.todoSection}>
                    {children}
                </div>
                <small>
                    Created: {todoList.createdDate?.substring(0, 10)}
                </small>
            </div>
        </div>
    );
}

export default TodoListBig;
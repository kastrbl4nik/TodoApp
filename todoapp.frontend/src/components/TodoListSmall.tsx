import { BsPlusCircle } from 'react-icons/bs';
import { TodoList } from '../models/models';
import styles from './TodoListSmall.module.css';

interface Props {
    todoList?: TodoList,
    onClick: () => void,
}

function TodoListSmall({todoList, onClick}: Props) {
    return (
        <button className={'box ' + styles.container} onClick={onClick}>
            <div className={styles.leftSide}>
                {todoList ? <>
                    <div className={styles.icon}>{todoList.title.substring(0,1)}</div>
                    <span>{todoList!.title}</span>
                </> : <>
                    <div className={styles.icon}><BsPlusCircle/></div>
                    <span>Create new todo list</span>
                </>}
            </div>
            {todoList ? <small>{todoList.createdDate?.substring(0, 10)}</small>: ''}
        </button>
    );
}

export default TodoListSmall;
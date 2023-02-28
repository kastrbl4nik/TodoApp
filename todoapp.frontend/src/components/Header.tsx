import { useState } from 'react';
import { BsPersonCircle } from 'react-icons/bs';
import styles from './Header.module.css';

interface Props {
    onSearch: (value: string) => void
}

function Header({onSearch}: Props) {
    return (
        <header className="box">
            <button className={styles.burger}>
                <div />
                <div />
                <div />
            </button>
            <input type="search" onChange={(e) => {
                onSearch(e.target.value);
            }}/>
            <div className={styles.avatar}>
                <BsPersonCircle />
            </div>
        </header>
    );
}

export default Header;
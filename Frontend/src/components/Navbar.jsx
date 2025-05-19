import React, { useState } from 'react'

import '../css/Navbar.css'

function Navbar() {

    const [contador, setContador] = useState(0);

    const [menu, setMenu] = useState(false);

    let variable = "HOLA MUNDO KEVIN!";

    return (
        <div className='div'>
            <h1>{variable}</h1>
            <h3>Contador: {contador}</h3>
            <button onClick={() => setContador(contador + 1)}>Voy a sumar!!</button>
        </div>
    )
}

export default Navbar
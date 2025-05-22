import React from 'react'

function CommonNavbar() {
    return (
        <nav className="navbar navbar-expand-lg" style={{ backgroundColor: "#65C56F" }}>
            <div className="container-fluid">
                {/* Logo y texto "AGRO KAJA" con botón radio */} 2
                <div className="d-flex align-items-center">
                    <input
                        type="radio"
                        id="agroKaja"
                        name="brand"
                        className="form-check-input me-2"
                        style={{ backgroundColor: "white" }}
                    />
                    <label
                        htmlFor="agroKaja"
                        className="navbar-brand mb-0 fs-5 fw-bold text-white"
                        style={{ cursor: "pointer" }}
                    >
                        AGRO KAJA
                    </label>
                </div>

                {/* Buscador */}
                <form className="d-flex flex-grow-1 mx-3">
                    <input
                        className="form-control rounded-pill"
                        type="search"
                        placeholder="Buscar productos..."
                        aria-label="Buscar productos"
                    />
                </form>

                {/* Enviar a, explorar, perfil, vender y carrito */}
                <div className="d-flex align-items-center text-white fw-bold" style={{ fontSize: "14px" }}>
                    <span className="me-4">
                        Enviar a <br />
                        <span style={{ fontWeight: "900" }}>calle 20 #02-03</span>
                    </span>
                    <span className="me-4" style={{ cursor: "pointer" }}>Explorar</span>
                    <span className="me-4" style={{ cursor: "pointer" }}>Mi perfil</span>
                    <span className="me-4" style={{ cursor: "pointer" }}>Vender</span>
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="white" className="bi bi-cart3" viewBox="0 0 16 16">
                        <path d="M0 1.5A.5.5 0 0 1 .5 1h1a.5.5 0 0 1 .485.379L2.89 5H14.5a.5.5 0 0 1 .491.592l-1.5 8A.5.5 0 0 1 13 14H4a.5.5 0 0 1-.491-.408L1.01 2H.5a.5.5 0 0 1-.5-.5zM3.102 6l1.313 7h7.17l1.313-7H3.102zM5 12a2 2 0 1 0 0 4 2 2 0 0 0 0-4zm6 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4z" />
                    </svg>
                </div>
            </div>
        </nav>
    )
}

export default CommonNavbar
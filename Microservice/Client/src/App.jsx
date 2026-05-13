/* eslint-disable react-hooks/immutability */
/* eslint-disable no-unused-vars */
// No import needed for React with new JSX transform


import { useState, useEffect } from 'react';
import './App.css';

function App() {
    const [products, setProducts] = useState([]);
    const [filteredProducts, setFilteredProducts] = useState([]);
    const [selectedColor, setSelectedColor] = useState('');
    const [colors, setColors] = useState([]);
    const [newProduct, setNewProduct] = useState({
        id: '', 
        name: '',
        color: '',
        price: '',
        details: '',
        occupancy: ''
    });
    const [isLogin, setIsLogin] = useState(false);
    const [token, setToken] = useState(localStorage.getItem('token') || '');
    const [isAuthenticated, setIsAuthenticated] = useState(!!localStorage.getItem('token'));
    const [loginData, setLoginData] = useState({ email: '', password: '' });
    const [registerData, setRegisterData] = useState({ email: '', name: '', password: '', role: '' });
    const [showRegister, setShowRegister] = useState(false);
    const [message, setMessage] = useState('');
    const [healthStatus, setHealthStatus] = useState('Checking...');

    const API_BASE = 'https://royalvilla-8.onrender.com';
    //const API_BASE =';

    useEffect(() => {
        fetch(`${API_BASE}/health`)
            .then(res => res.ok ? setHealthStatus('Healthy') : setHealthStatus('Unhealthy'))
            .catch(() => setHealthStatus('Offline'));
        fetchProductsAnon();
    }, []);

    useEffect(() => {
        if (isAuthenticated && token) {
            fetchProducts();
        }
    }, [isAuthenticated, token]);


    const fetchProductsAnon = async () => {
        try {
            const response = await fetch(`${API_BASE}/api/villa`);
            const data = await response.json();
            //console.log(data);
            if (data) {
               setProducts(data);               
            }
        } catch (error) {
            setMessage('Error fetching products');
        }
    };


    const fetchProducts = async () => {
        try {
            const response = await fetch(`${API_BASE}/api/villa`, {
                headers: { 'Authorization': `Bearer ${token}` }
            });
            const data = await response.json();
            //console.log(data);
            if (data) {
                
                setProducts(data);
                const uniqueColors = [...new Set(data.map(p => p.color).filter(c => c))];
                setColors(uniqueColors);
            }
        } catch (error) {
            setMessage('Error fetching products');
        }
    };

    const handleLogin = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch(`${API_BASE}/api/auth/Login`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(loginData)
            });
            const data = await response.json();
            console.log(data,data.data,data.success);
            if (data.success && data.data && data.data.token) {
                const newToken = data.data.token;
                localStorage.setItem('token', newToken);
                setToken(newToken);
                setIsAuthenticated(true);
                setMessage('Login successful!');
                setLoginData({ email: '', password: '' });
            } else {
                setMessage('Login failed');
            }
        } catch (error) {
            setMessage('Login error');
        }
    };

    const handleRegister = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch(`${API_BASE}/api/auth/Register`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(registerData)
            });
            const data = await response.json();
            if (response.ok && data.success === true) {
                setMessage('Registration successful! Please login.');
                setShowRegister(false);
                setRegisterData({ email: '', name: '', password: '', role: '' });
            } else {
                setMessage('Registration failed');
            }
        } catch (error) {
            setMessage('Registration error');
        }

    };

    const clearNewProduct = () => {
        console.log(newProduct);
        setNewProduct({ id: '', name: '', color: '', price: '', details: '', occupancy: '' });
        console.log(newProduct);
    };

    const handleEditProduct = async (e) => {
        e.preventDefault();


        const updatedData = {
            id: newProduct.id,
            name: newProduct.name,
            color: newProduct.color,
            price: newProduct.price,
            details: newProduct.details,
            occupancy: newProduct.occupancy
        };

        try {
            const response = await fetch(`${API_BASE}/api/villa/${updatedData.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify(updatedData)
            });
            const data = await response.json();
            if (data.success) {
                setMessage('Product updated successfully!');
                setNewProduct({id:'', name: '', color: '', price: '', details: '', occupancy: '' });
                fetchProducts();
            } else {
                setMessage('Failed to update product');
            }
        } catch (error) {
            setMessage('Error updating product');
        }
    };

    const handleCreateProduct = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch(`${API_BASE}/api/villa`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify(newProduct)
            });
            const data = await response.json();
            if (data.id) {
                setMessage('Product created successfully!');
                setNewProduct({ name: '', color: '', price: '', details: '', occupancy: '' });
                fetchProducts();
            } else {
                setMessage('Failed to create product');
            }
        } catch (error) {
            setMessage('Error creating product');
        }
    };

    const filterByColor = async (color) => {
        setSelectedColor(color);
        if (color === '') {
            setFilteredProducts([]);
            return;
        }
        try {
            const response = await fetch(`${API_BASE}/api/villa/color/${color}`, {
                headers: { 'Authorization': `Bearer ${token}` }
            });
            const data = await response.json();
            if (data) {
                setFilteredProducts(data);
            }
        } catch (error) {
            setMessage('Error filtering products');
        }
    };

    const handleAuthPage = () => {
        setIsLogin(!isLogin);
        setShowRegister(false);
    };

    const handleEdit = async (id) => {
        try {

            const response = await fetch(`${API_BASE}/api/villa/${id}`, {
                headers: { 'Authorization': `Bearer ${token}` }
            });
            const data = await response.json();
            const {createdDate, updatedDate, ...updatedData } = data
            console.log(updatedData);
            setNewProduct(updatedData);
            
        } catch (e) { setMessage(e.message || 'Failed to fecth villa')        }
    };

    const handleDelete = async (id) => {
        try {

            const response = await fetch(`${API_BASE}/api/villa/${id}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`

                }
            });
            const data = await response.json();
            console.log(data);
            if (data.success) { 
            setMessage(data.message);
            }
        } catch (e) { setMessage(e.message || 'Failed to delete'); }
    };

    const handleLogout = () => {
        localStorage.removeItem('token');
        setToken('');
        setIsAuthenticated(false);
        setProducts([]);
        setFilteredProducts([]);
        setMessage('Logged out');
    };

    if (!isAuthenticated) {
        if (!isLogin) {
            return (
                <div className="container">
                    <div className="header">
                        <h1>Products </h1>
                        <div className="health-status">
                            API Health:
                            <span className={healthStatus === 'Healthy' ? 'healthy' : 'unhealthy'}>
                                {healthStatus}
                            </span>
                        </div>
                        <button onClick={handleAuthPage} className="login-page-btn">Login</button>
                    </div>

                    {message && <div className="message">{message}</div>}


                    <div className="products-container">
                        <h2>Products</h2>
                        <div className="products-grid">
                            {(products).map(product => (
                                <div key={product.id} className="product-card">
                                    <h3>{product.name}</h3>
                                    <p><strong>Color:</strong> {product.color}</p>
                                    <p><strong>Price:</strong> ${product.price}</p>
                                    <p><strong>Details:</strong> {product.details}</p>
                                    <p><strong>Occupancy:</strong> {product.occupancy}</p>
                                    {product.createdDate && (
                                        <small>Created: {new Date(product.createdDate).toLocaleDateString()}</small>
                                    )}
                                </div>
                            ))}
                        </div>

                        {(products).length === 0 && (
                            <p>No products found</p>
                        )}
                    </div>
                </div>
            );
        }
       else {
            return (
            <div className="container">
                    <div className="header">
                        <h1>Products </h1>
                        <div className="health-status">
                            API Health:
                            <span className={healthStatus === 'Healthy' ? 'healthy' : 'unhealthy'}>
                                {healthStatus}
                            </span>
                        </div>
                        <button onClick={handleAuthPage} className="login-page-btn">See Villas</button>
                    </div>
                <div className="auth-container">
                    <div className="auth-box">
                        <h2>{showRegister ? 'Register' : 'Login'}</h2>
                        {message && <div className="message">{message}</div>}

                        {!showRegister ? (
                            <form onSubmit={handleLogin}>
                                <input
                                    type="email"
                                    placeholder="Email"
                                    value={loginData.email}
                                    onChange={(e) => setLoginData({ ...loginData, email: e.target.value })}
                                    required
                                />
                                <input
                                    type="password"
                                    placeholder="Password"
                                    value={loginData.password}
                                    onChange={(e) => setLoginData({ ...loginData, password: e.target.value })}
                                    required
                                />
                                <button type="submit">Login</button>
                            </form>
                        ) : (
                            <form onSubmit={handleRegister}>
                                <input
                                    type="email"
                                    placeholder="Email"
                                    value={registerData.email}
                                    onChange={(e) => setRegisterData({ ...registerData, email: e.target.value })}
                                    required
                                />
                                <input
                                    type="text"
                                    placeholder="Name"
                                    value={registerData.name}
                                    onChange={(e) => setRegisterData({ ...registerData, name: e.target.value })}
                                    required
                                />
                                <input
                                    type="password"
                                    placeholder="Password"
                                    value={registerData.password}
                                    onChange={(e) => setRegisterData({ ...registerData, password: e.target.value })}
                                    required
                                />
                                <input
                                    type="text"
                                    placeholder="Role"
                                    value={registerData.role}
                                    onChange={(e) => setRegisterData({ ...registerData, role: e.target.value })}
                                />
                                <button type="submit">Register</button>
                            </form>
                        )}

                        <button className="toggle-btn" onClick={() => setShowRegister(!showRegister)}>
                            {showRegister ? 'Back to Login' : 'Need an account? Register'}
                        </button>
                    </div>
                </div>
            </div>
        );
      }
    }

    return (
        <div className="container">
            <div className="header">
                <h1>Products Management</h1>
                <div className="health-status">
                    API Health: <span className={healthStatus === 'Healthy' ? 'healthy' : 'unhealthy'}>{healthStatus}</span>
                </div>
                <button onClick={handleLogout} className="logout-btn">Logout</button>
            </div>

            {message && <div className="message">{message}</div>}

            <div className="form-container">
                <h2>{newProduct.id ? 'Update Product' : 'Create New Product' }</h2>
                <form onSubmit={newProduct.id ? handleEditProduct : handleCreateProduct}>
               

                    <input
                        type="text"
                        placeholder="Name"
                        value={newProduct.name}
                        onChange={(e) => setNewProduct({ ...newProduct, name: e.target.value })}
                        required
                    />
                    <input
                        type="text"
                        placeholder="Color"
                        value={newProduct.color}
                        onChange={(e) => setNewProduct({ ...newProduct, color: e.target.value })}
                        required
                    />
                    <input
                        type="number"
                        placeholder="Price"
                        value={newProduct.price}
                        onChange={(e) => setNewProduct({ ...newProduct, price: e.target.value })}
                        required
                    />
                    <input
                        type="text"
                        placeholder="Details"
                        value={newProduct.details}
                        onChange={(e) => setNewProduct({ ...newProduct, details: e.target.value })}
                    />
                    <input
                        type="number"
                        placeholder="Occupancy"
                        value={newProduct.occupancy}
                        onChange={(e) => setNewProduct({ ...newProduct, occupancy: e.target.value })}
                        required
                    />
                    <p className="btn-contain" > 
                        <button className="btn-success" type="submit">{newProduct.id ? 'Update Product' : 'Create Product'}</button>
                        {newProduct.id ? <button className="btn-danger" onClick={clearNewProduct} > Cancel Update </button> : "" }

                    </p>

                </form>
            </div>

            <div className="filter-container">
                <h3>Filter by Color</h3>
                <select value={selectedColor} onChange={(e) => filterByColor(e.target.value)}>
                    <option value="">All Colors</option>
                    {colors.map(color => (
                        <option key={color} value={color}>{color}</option>
                    ))}
                </select>
            </div>

            <div className="products-container">
                <h2>Products</h2>
                <div className="products-grid">
                    {(selectedColor ? filteredProducts : products).map(product => (
                        <div key={product.id} className="product-card">
                            <h3>{product.name}</h3>
                            <p><strong>Color:</strong> {product.color}</p>
                            <p><strong>Price:</strong> ${product.price}</p>
                            <p><strong>Details:</strong> {product.details}</p>
                            <p><strong>Occupancy:</strong> {product.occupancy}</p>
                            {product.createdDate && (
                                <small>Created: {new Date(product.createdDate).toLocaleDateString()}</small>
                            )}

                            <p   className="btn-holder">
                                <small onClick={() => handleEdit(product.id)} className="btn-edit"  >Edit</small>
                                <small onClick={() => handleDelete(product.id)} className="btn-delete">Delete</small>
                            </p>

                        </div>
                    ))}
                </div>
                {(selectedColor ? filteredProducts : products).length === 0 && (
                    <p>No products found</p>
                )}
            </div>
        </div>
    );
}

export default App;
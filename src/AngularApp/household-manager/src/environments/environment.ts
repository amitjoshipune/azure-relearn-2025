export const environment = {
  production: false,
  api: {
    auth: 'http://localhost:5000/api/auth',
    products: 'http://localhost:5001/api/product',
    todos: 'http://localhost:5003/api/todo',
    stock: 'http://localhost:5002/api/stock',
    requisition: 'http://localhost:5004/api/requisition'
  },
  tokenKey: 'access_token'
};

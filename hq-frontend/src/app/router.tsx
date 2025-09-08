import { Navigate, Route, Routes } from 'react-router-dom';
import Layout from '../shared/layout/Layout'
import { publicRoutes, privateRoutes } from '../shared/config/routes';


export const Router = () => (
    <Routes>
      <Route path="/" element={<Layout />} >
        {privateRoutes.map((r) => (
          <Route key={r.path} path={r.path} element={<r.component/>} />
        ))}
        {publicRoutes.map((r) => (
          <Route key={r.path} path={r.path} element={<r.component />} />
        ))}
        <Route path="*" element={<Navigate to='/' />} />
      </Route>
    </Routes>  
);

import { Route, Routes } from 'react-router-dom';
import { HomePage } from '../pages/HomePage';
import Header from '../shared/components/Header'
import {GetAllJuries} from '../features/contest/jury/GetAllJuries'
import Layout from '../shared/layout/Layout'


export const Router = () => (
  <Routes>
    <Route path="/" element={<Layout />} >
      <Route index element={<HomePage/>} />
      {/* добавляешь другие страницы здесь */}
      <Route path="api/" element = {<GetAllJuries/>} />
    </Route>
  </Routes>
);

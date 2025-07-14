import { Route, Routes } from 'react-router-dom';
import { HomePage } from '../pages/HomePage';

export const Router = () => (
  <Routes>
    <Route path="/" element={<HomePage/>} />
    {/* добавляешь другие страницы здесь */}
  </Routes>
);

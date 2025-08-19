import { Box, Divider } from "@mui/material";
import Header from '../components/Header'
import Footer from "../components/Footer";
import { Outlet } from "react-router-dom";

export default function Layout() {
  return (
    <Box sx={{ display: "flex", flexDirection: "column", minHeight: "100vh" }}>
      <Header/>

      {/* Здесь будут меняться страницы */}
      <Box component="main" sx={{ flex: 1, p: 3 }}>
        <Outlet />  
      </Box>

      <Footer />
    </Box>
  );
}

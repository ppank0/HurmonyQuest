import {Box} from "@mui/material"
import { createNomination } from "./api";
import { useState } from 'react'

export const CreateNomination = () => {
    const [form, setForm] = useState({ name: ""});

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await createNomination(form);
      alert("Пользователь создан!");
    } catch (err) {
      alert("Ошибка: " + err);
    }
  };

    return (
        <Box>
            <form onSubmit={handleSubmit}>
                <input name="name" value={form.name} onChange={handleChange} placeholder="Имя" />
                <button type="submit">Создать</button>
            </form>
        </Box>
    )
}
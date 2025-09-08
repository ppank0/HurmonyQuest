import { JuryItem } from "../../../types/types";
import { getJuries } from "./api"
import { DataTable } from "../crud/DataTable"

export const GetAllJuries = () => {

    return (
        <DataTable<JuryItem> 
                fetchData={getJuries}
                columns={[
                    { header: 'Name', field: 'name' },
                    { header: 'Surname', field: 'surname' },
                    { header: 'Birthday', field: 'birthday' },
                ]}
         />
    );
}

import { DataTable } from "../crud/DataTable";
import { ParticipantItem } from "../../../types/types";
import { getParticipants } from "./api";

export const GetAllParticipants = () => {
  return (
    <DataTable<ParticipantItem>
      fetchData={getParticipants}
      columns={[
        { header: "Name", field: "name" },
        { header: "Surname", field: "surname" },
        { header: "Birthday", field: "birthday" },
        { header: "musical instrument",
          field: "musicalInstrumentName"},
        { header: "nominationId", field: "nominationId"}
      ]}
    />
  );
};
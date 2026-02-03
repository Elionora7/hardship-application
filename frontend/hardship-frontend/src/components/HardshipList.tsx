import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { deleteHardshipApplication, getHardshipApplications } from "../api/hardshipApi";
import type { HardshipApplication } from "../types/hardship";

interface Props {
  onEdit: (app: HardshipApplication) => void;
}

export function HardshipList({ onEdit }: Props) {
  const queryClient = useQueryClient();

  const { data, isLoading, isError } = useQuery({
    queryKey: ["hardships"],
    queryFn: getHardshipApplications,
  });

  const deleteMutation = useMutation({
    mutationFn: deleteHardshipApplication,
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ["hardships"] }),
  });

  if (isLoading) return <p>Loading applications...</p>;
  if (isError) return <p>Error loading applications</p>;

  return (
    <div className="card list-card">
      <h2>Hardship Applications</h2>

      <ul>
        {data && data.length > 0 ? (
          data.map(app => (
            <li key={app.id} className="list-item">
              <div>
                <strong>{app.customerName}</strong>
                <div>Income: {app.income}</div>
                <div>Expenses: {app.expenses}</div>
                {app.hardshipReason && <div>Reason: {app.hardshipReason}</div>}
              </div>

              <div style={{ display: "flex", gap: 8 }}>
                <button onClick={() => onEdit(app)}>Edit</button>
                <button
                  onClick={() => {
                    if (confirm(`Delete ${app.customerName}'s application?`)) {
                      deleteMutation.mutate(app.id);
                    }
                  }}
                >
                  Delete
                </button>
              </div>
            </li>
          ))
        ) : (
          <li>No hardship applications found</li>
        )}
      </ul>
    </div>
  );
}

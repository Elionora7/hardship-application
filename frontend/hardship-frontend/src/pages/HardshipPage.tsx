import { useState } from "react";
import type { HardshipApplication } from "../types/hardship";
import { HardshipForm } from "../components/HardshipForm";
import { HardshipList } from "../components/HardshipList";

export function HardshipPage() {
  const [editing, setEditing] = useState<HardshipApplication | null>(null);

  const clearEditing = () => setEditing(null);

  return (
    <div style={{ maxWidth: 800, margin: "0 auto", padding: 24 }}>
      <h1>Hardship Application System</h1>

      <HardshipForm selected={editing} onClear={clearEditing} />

      <hr style={{ margin: "24px 0" }} />

      <HardshipList onEdit={setEditing} />
    </div>
  );
}

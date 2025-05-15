
# 🧭 Projet Unity : Simulation de Tâches de Matelot

## 🎯 Objectif

Ce projet propose une simulation simple dans Unity où un matelot effectue des tâches successives.  
Il devient fatigué après un certain nombre de tâches et doit se reposer avant de pouvoir retravailler.

---

## 🛠️ Fonctionnalités

- Matelot cliquable avec souris (via raycast)
- États du matelot : **Disponible**, **Travaille**, **Fatigué**
- Affichage de l’état sous forme de texte au-dessus du personnage
- Barre de progression pendant la tâche (Slider)
- Système de fatigue après un nombre configurable de tâches
- Repos automatique de 10 secondes avant de redevenir disponible

---

## 🧱 Architecture

### 🎲 Pattern utilisé : **Machine à États (State Pattern)**

Le comportement du matelot est structuré en trois états :
- `Idle` : Le matelot est disponible pour une tâche
- `Working` : Le matelot effectue une tâche pendant une durée déterminée
- `Tired` : Le matelot est fatigué, et reste inactif pendant une période de repos

Chaque état est géré dans une structure `switch` dans la méthode `Update()`, simulant un **State Machine** simple sans classe abstraite.  
La méthode `ChangeState(State)` centralise les transitions entre états.

---

## 🧩 Structure des scripts

- `Sailor.cs` : Script principal gérant les états du matelot, les transitions, le clic utilisateur et les timers.
- L’UI est configurée dans l’inspecteur Unity avec :
  - Un `Slider` pour la barre de progression
  - Un `TextMeshPro` pour afficher l’état
  - Un `GameObject` parent activé/désactivé dynamiquement pour contrôler l'affichage

---

## 📦 Packages & Librairies

- **TextMeshPro** : pour l’affichage du texte (remplace `UnityEngine.UI.Text` pour une meilleure qualité)
  - Namespace utilisé : `using TMPro`
  - Nécessite l'importation de TMP via l’UI ou le Package Manager (généralement préinstallé dans Unity)
  - Boats Pack : navire sur lequel travaillent les Matelots

---

## 📂 Version et compatibilité

- ✅ Unity 2021.3.33f1 ou version supérieure
- 📱 Compatible PC / Android / WebGL (pas encore testé sur mobile)

---

## 📸 Aperçu du comportement

| État          | Comportement utilisateur     | Résultat visuel                   |
|---------------|------------------------------|------------------------------------|
| Disponible    | Clic sur matelot             | Barre apparaît, tâche commence     |
| Effectue tâche| Attente                      | Barre se remplit, texte mis à jour |
| Fatigué       | 5 tâches faites, puis clic   | Texte "Fatigué", attente 10s       |

---

## ✨ Bonus possibles

- Animation de repos du matelot
- Multiples matelots avec gestion indépendante
- UI flottante toujours face à la caméra
- Son ou feedback visuel lors de changement d’état

---

## ✍️ Auteur

Créé par Merveille NEDUMLUBA-ANG
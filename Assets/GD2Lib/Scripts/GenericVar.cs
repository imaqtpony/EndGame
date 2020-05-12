using System;
using UnityEngine;

namespace GD2Lib {

    /// <summary>
    /// Classe abstraite pour les variables synchronisées
    /// </summary>
    /// <typeparam name="T">Type de variable synchonisé</typeparam>
    public abstract class GenericVar<T> : ScriptableObject {

        [Tooltip("Valeur de la donnée au démarrage de l'application. (non réutilisé entre les play dans l'editeur)")]
        [SerializeField] T m_initialValue;

        /// <summary>
        /// Valeur runtime de la donnée
        /// </summary>
        [NonSerialized] protected T m_runtimeValue;

        /// <summary>
        /// Valeur de la donnée
        /// </summary>
        public T Value {
            get { return m_runtimeValue; }
            set { SetValue(value); }
        }

        /// <summary>
        /// Evenement de changement de la valeur
        /// </summary>
        public event Action<T> OnValueChanged;

        /// <summary>
        /// Fixe la valeur
        /// </summary>
        /// <param name="p_value">Nouvelle valeur</param>
        protected virtual void SetValue(T p_value) {
            m_runtimeValue = p_value;
            OnValueChanged?.Invoke(m_runtimeValue);
        }
    }
}



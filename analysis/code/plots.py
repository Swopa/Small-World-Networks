import pandas as pd
import matplotlib.pyplot as plt
import numpy as np

ws = pd.read_csv('../data/ws_results.csv')

ws_avg = ws.groupby('p').agg(
    avg_L=('avg_path_length', 'mean'),
    avg_C=('clustering_coefficient', 'mean')
).reset_index()

ws_avg['L_norm'] = ws_avg['avg_L'] / ws_avg['avg_L'].max()
ws_avg['C_norm'] = ws_avg['avg_C'] / ws_avg['avg_C'].max()

plt.figure(figsize=(8, 5))
plt.semilogx(ws_avg['p'], ws_avg['L_norm'], 'b-o', label='L(p) / L(0)')
plt.semilogx(ws_avg['p'], ws_avg['C_norm'], 'b-o', label='C(p) / C(0)')
plt.xlabel('Rewiring Probability p')
plt.ylabel('Normalized Value')
plt.title('Watts-Strogatz Small-World Properties')
plt.legend()
plt.grid(True)
plt.savefig('../data/ws_plot.png', dpi=150)
plt.show()

k = pd.read_csv('../data/kleinberg_torus.csv')

k_success = k[k['hops'] != -1]
k_failed = k[k['hops'] == -1]

k_avg = k_success.groupby('r').agg(
    avg_hops = ('hops', 'mean')
).reset_index()

total_per_r = k.groupby('r').size()
failed_per_r = k_failed.groupby('r').size().reindex(total_per_r.index, fill_value=0)
failure_rate = (failed_per_r / total_per_r).reset_index()
failure_rate.columns = ['r', 'failure_rate']

plt.figure(figsize=(8, 5))
plt.plot(k_avg['r'], k_avg['avg_hops'], 'b-o', label = 'Average Hops')
plt.axvline(x=2, color='r', linestyle ='--', label='r=2 (theoretical optimum)')
plt.xlabel('Exponent r')
plt.ylabel('Average Hops')
plt.title('Kleinberg Greedy Routing Performance')
plt.legend()
plt.grid(True)
plt.savefig('../data/graphs/kleinberg_torus_plot.png', dpi=150)
plt.show()